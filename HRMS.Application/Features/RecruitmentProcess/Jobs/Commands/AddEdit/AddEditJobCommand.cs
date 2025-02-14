using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.AddEdit;
using HRMS.Application.FeatureValidators.RecruitmentProcess;
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

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Commands.AddEdit
{
    public class AddEditJobCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Salary { get; set; }
        public string NoOfVacancy { get; set; }
        public string Status { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public int JobCategoryId { get; set; }
        public int JobLocationId { get; set; }
        public int CompanyId { get; set; }
    }
    internal class AddEditJobCommandHandler : IRequestHandler<AddEditJobCommand, Result<int>>
    {
        private readonly ILogger<AddEditJobCommandHandler> _logger;
        private readonly IStringLocalizer<AddEditJobCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEditJobCommand> _addEditJobCommandValidator;

        public AddEditJobCommandHandler(ILogger<AddEditJobCommandHandler> logger, IStringLocalizer<AddEditJobCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper, IValidator<AddEditJobCommand> addEditJobCommandValidator)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addEditJobCommandValidator = addEditJobCommandValidator;
        }

        public async Task<Result<int>> Handle(AddEditJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Job Add/Update Validation Started");
                var validationResult = _addEditJobCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Job Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("Job Mapping to DTO for New Record");
                        var dept = _mapper.Map<Job>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("Job {Title} is Staring Adding to Jobs", dept.Title);
                            var res = await _unitOfWork.Repository<Job>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Job {Title} is Added Successfully", res.Title);
                            return await Result<int>.SuccessAsync(res.Id, _localizer["Job Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map Job Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing Job Data...");
                        var dept = await _unitOfWork.Repository<Job>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                            dept.Title = string.IsNullOrWhiteSpace(request.Title) ? dept.Title : request.Title;
                            dept.Description = request.Description;
                            dept.NoOfVacancy = request.NoOfVacancy;
                            dept.Salary = request.Salary;
                            dept.CompanyId = request.CompanyId;
                            dept.JobCategoryId = request.JobCategoryId;
                            dept.JobLocationId = request.JobLocationId;
                            dept.Status = request.Status;
                            dept.LastApplicationDate = request.LastApplicationDate;
                            dept.Status = request.Status;
                            dept.Type = request.Type;
                            _logger.LogInformation("Updating Job Data...");
                            await _unitOfWork.Repository<Job>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Job Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localizer["Job Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find Job");
                        }
                    }
                }
                else
                {
                    _logger.LogError("Job Validation Failed {0}", validationResult.Errors);
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
