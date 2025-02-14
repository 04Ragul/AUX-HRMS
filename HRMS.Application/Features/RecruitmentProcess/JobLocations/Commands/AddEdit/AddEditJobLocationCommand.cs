using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.AddEdit
{
    public class AddEditJobLocationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
    internal class AddEditJobLocationCommandHandler : IRequestHandler<AddEditJobLocationCommand, Result<int>>
    {
        private readonly ILogger<AddEditJobLocationCommandHandler> _logger;
        private readonly IStringLocalizer<AddEditJobLocationCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEditJobLocationCommand> _addEditJobLocationCommandValidator;

        public AddEditJobLocationCommandHandler(ILogger<AddEditJobLocationCommandHandler> logger, IStringLocalizer<AddEditJobLocationCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper, IValidator<AddEditJobLocationCommand> addEditJobLocationCommandValidator)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addEditJobLocationCommandValidator = addEditJobLocationCommandValidator;
        }

        public async Task<Result<int>> Handle(AddEditJobLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("JobLocation Add/Update Validation Started");
                var validationResult = _addEditJobLocationCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("JobLocation Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("JobLocation Mapping to DTO for New Record");
                        var dept = _mapper.Map<JobLocation>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("JobLocation {Country},{State} is Staring Adding to JobLocations", dept.Country, dept.State);
                            var res = await _unitOfWork.Repository<JobLocation>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobLocationCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("JobLocation {Country},{State} is Staring Adding to JobLocations", res.Country, res.State);
                            return await Result<int>.SuccessAsync(res.Id, _localizer["JobLocation Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map JobLocation Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing JobLocation Data...");
                        var dept = await _unitOfWork.Repository<JobLocation>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                            dept.State = request.State;
                            dept.Country = request.Country;
                            dept.Address = request.Address;
                            dept.City = request.City;

                            _logger.LogInformation("Updating JobLocation Data...");
                            await _unitOfWork.Repository<JobLocation>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobLocationCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("JobLocation Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localizer["JobLocation Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find JobLocation");
                        }
                    }
                }
                else
                {
                    _logger.LogError("JobLocation Validation Failed {0}", validationResult.Errors);
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
