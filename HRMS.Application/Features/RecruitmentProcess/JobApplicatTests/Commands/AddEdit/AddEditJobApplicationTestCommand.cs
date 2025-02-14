using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Recruitment;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatTests.Commands.AddEdit
{
    public class AddEditJobApplicationTestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public string Status { get; set; }
    }

    internal class AddEditJobApplicationTestCommandHandler : IRequestHandler<AddEditJobApplicationTestCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditJobApplicationTestCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditJobApplicationTestCommandHandler> _logger;
        private readonly IValidator<AddEditJobApplicationTestCommand> _validator;

        public AddEditJobApplicationTestCommandHandler(
            IStringLocalizer<AddEditJobApplicationTestCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<AddEditJobApplicationTestCommandHandler> logger,
            IValidator<AddEditJobApplicationTestCommand> validator)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<int>> Handle(AddEditJobApplicationTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Job Application Test Add/Update Validation Started");

                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation Failed: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return await Result<int>.FailAsync(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation error");
                }

                _logger.LogInformation("Validation Succeeded");

                if (request.Id == 0)
                {
                    _logger.LogInformation("Mapping to DTO for new job application test record");
                    var jobApplicationTest = _mapper.Map<JobApplicationTest>(request);

                    _logger.LogInformation("Adding Job Application Test {TestName} for Applicant {ApplicantId}", jobApplicationTest.TestName, jobApplicationTest.ApplicantId);
                    var result = await _unitOfWork.Repository<JobApplicationTest>().AddAsync(jobApplicationTest);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationTestsCacheKey);

                    _logger.LogInformation("Job Application Test {TestName} added successfully", result.TestName);
                    return await Result<int>.SuccessAsync(result.Id, _localizer["Job Application Test Saved"]);
                }
                else
                {
                    _logger.LogInformation("Fetching existing job application test data for ID {Id}", request.Id);
                    var jobApplicationTest = await _unitOfWork.Repository<JobApplicationTest>().GetByIdAsync(request.Id);

                    if (jobApplicationTest == null)
                    {
                        _logger.LogError("Job Application Test not found for ID {Id}", request.Id);
                        return await Result<int>.FailAsync("Job Application Test not found");
                    }

                    jobApplicationTest.TestName = request.TestName;
                    jobApplicationTest.TestDate = request.TestDate;
                    jobApplicationTest.Status = request.Status;

                    _logger.LogInformation("Updating job application test data...");
                    await _unitOfWork.Repository<JobApplicationTest>().UpdateAsync(jobApplicationTest);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationTestsCacheKey);

                    _logger.LogInformation("Job Application Test updated successfully");
                    return await Result<int>.SuccessAsync(jobApplicationTest.Id, _localizer["Job Application Test Updated"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing job application test add/edit: {Message}", ex.Message);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}