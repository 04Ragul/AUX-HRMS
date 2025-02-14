using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Recruitment;
using HRMS.Domain.Enums;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplication.Commands.AddEdit
{
    public class AddEditJobApplicationCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Contact { get; set; }
        public string ProfessionalSummary { get; set; }
        public string HighestEducation { get; set; }
        public string ProfileImage { get; set; }
        public string Resume { get; set; }
        public GenderType Gender { get; set; }
        public int ApplicantId { get; set; }
        public int JobId { get; set; }
        public string ApplicationStatus { get; set; }
    }

    internal class AddEditJobApplicationCommandHandler : IRequestHandler<AddEditJobApplicationCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditJobApplicationCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditJobApplicationCommandHandler> _logger;
        private readonly IValidator<AddEditJobApplicationCommand> _validator;

        public AddEditJobApplicationCommandHandler(
            IStringLocalizer<AddEditJobApplicationCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<AddEditJobApplicationCommandHandler> logger,
            IValidator<AddEditJobApplicationCommand> validator)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(AddEditJobApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Job Application Add/Update Validation Started");

                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Job Application Validation Failed: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return await Result<int>.FailAsync(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation error");
                }

                _logger.LogInformation("Job Application Validation Succeeded");

                if (request.ApplicantId == 0)
                {
                    _logger.LogInformation("Mapping to DTO for new job application record");
                    var jobApplication = _mapper.Map<JobApplication>(request);

                    _logger.LogInformation("Adding Job Application for {Name}", jobApplication.Name);
                    var result = await _unitOfWork.Repository<JobApplication>().AddAsync(jobApplication);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationsCacheKey);

                    _logger.LogInformation("Job Application for {Name} added successfully", result.Name);
                    return await Result<int>.SuccessAsync(result.Id, _localizer["Job Application Saved"]);
                }
                else
                {
                    _logger.LogInformation("Fetching existing job application data for Applicant ID {ApplicantId}", request.ApplicantId);
                    var jobApplication = await _unitOfWork.Repository<JobApplication>().GetByIdAsync(request.ApplicantId);

                    if (jobApplication == null)
                    {
                        _logger.LogError("Job Application not found for Applicant ID {ApplicantId}", request.ApplicantId);
                        return await Result<int>.FailAsync("Job Application not found");
                    }

                    jobApplication.Name = request.Name;
                    jobApplication.EmailId = request.EmailId;
                    jobApplication.Contact = request.Contact;
                    jobApplication.ProfessionalSummary = request.ProfessionalSummary;
                    jobApplication.HighestEducation = request.HighestEducation;
                    jobApplication.ProfileImage = request.ProfileImage;
                    jobApplication.Resume = request.Resume;
                    jobApplication.Gender = request.Gender;
                    jobApplication.JobId = request.JobId;
                    jobApplication.ApplicationStatus = request.ApplicationStatus;

                    _logger.LogInformation("Updating job application data...");
                    await _unitOfWork.Repository<JobApplication>().UpdateAsync(jobApplication);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationsCacheKey);

                    _logger.LogInformation("Job Application updated successfully");
                    return await Result<int>.SuccessAsync(jobApplication.Id, _localizer["Job Application Updated"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing job application add/edit: {Message}", ex.Message);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}