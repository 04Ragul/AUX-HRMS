using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.Rounds.Commands.AddEdit;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features.Recruitment;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Commands.AddEdit
{
    public class AddEditRoundCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobCategoryId { get; set; }
    }
    internal class AddEditRoundCommandHandler : IRequestHandler<AddEditRoundCommand, Result<int>>
    {
        private readonly ILogger<AddEditRoundCommandHandler> _logger;
        private readonly IStringLocalizer<AddEditRoundCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEditRoundCommand> _AddEditRoundCommandValidator;

        public AddEditRoundCommandHandler(ILogger<AddEditRoundCommandHandler> logger, IStringLocalizer<AddEditRoundCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper, IValidator<AddEditRoundCommand> AddEditRoundCommandValidator)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _AddEditRoundCommandValidator = AddEditRoundCommandValidator;
        }
        public async Task<Result<int>> Handle(AddEditRoundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Round Add/Update Validation Started");
                var validationResult = _AddEditRoundCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Round Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("Round Mapping to DTO for New Record");
                        var dept = _mapper.Map<Round>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("Round {Title} is Staring Adding to Rounds", dept.Name);
                            var res = await _unitOfWork.Repository<Round>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRoundCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Round {Title} is Added Successfully", res.Name);
                            return await Result<int>.SuccessAsync(res.Id, _localizer["Round Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map Round Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing Round Data...");
                        var dept = await _unitOfWork.Repository<Round>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                           
                            dept.Name = request.Name;
                            dept.JobCategoryId = request.JobCategoryId;
                           
                            _logger.LogInformation("Updating Round Data...");
                            await _unitOfWork.Repository<Round>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRoundCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Round Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localizer["Round Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find Round");
                        }
                    }
                }
                else
                {
                    _logger.LogError("Round Validation Failed {0}", validationResult.Errors);
                    return await Result<int>.FailAsync(validationResult!.Errors!.FirstOrDefault()!.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}
