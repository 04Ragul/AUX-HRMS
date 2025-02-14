using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.FeatureValidators;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit
{
    public class AddEditJobCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
    }
    internal class AddEditJobCategoryCommandHandler : IRequestHandler<AddEditJobCategoryCommand, Result<int>>
    {
        private readonly ILogger<AddEditJobCategoryCommandHandler> _logger;
        private readonly IStringLocalizer<AddEditJobCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEditJobCategoryCommand> _addEditJobCategoryCommandValidator;

        public AddEditJobCategoryCommandHandler(ILogger<AddEditJobCategoryCommandHandler> logger, IStringLocalizer<AddEditJobCategoryCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper, IValidator<AddEditJobCategoryCommand> addEditJobCategoryCommandValidator)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addEditJobCategoryCommandValidator = addEditJobCategoryCommandValidator;
        }

        public async Task<Result<int>> Handle(AddEditJobCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("JobCategory Add/Update Validation Started");
                var validationResult = _addEditJobCategoryCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("JobCategory Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("JobCategory Mapping to DTO for New Record");
                        var dept = _mapper.Map<JobCategory>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("JobCategory {Name} is Staring Adding to JobCategorys", dept.Name);
                            var res = await _unitOfWork.Repository<JobCategory>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCategoryCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("JobCategory {Name} is Added Successfully", res.Name);
                            return await Result<int>.SuccessAsync(res.Id, _localizer["JobCategory Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map JobCategory Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing JobCategory Data...");
                        var dept = await _unitOfWork.Repository<JobCategory>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                            dept.Name = request.Name;
                            _logger.LogInformation("Updating JobCategory Data...");
                            await _unitOfWork.Repository<JobCategory>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCategoryCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("JobCategory Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localizer["JobCategory Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find JobCategory");
                        }
                    }
                }
                else
                {
                    _logger.LogError("JobCategory Validation Failed {0}", validationResult.Errors);
                    return await Result<int>.FailAsync(validationResult!.Errors!.FirstOrDefault()!.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}
