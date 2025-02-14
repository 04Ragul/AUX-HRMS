using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HRMS.Application.Interfaces.Repositories;
using Microsoft.Extensions.Localization;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplication.Commands.Delete
{
    public class DeleteJobApplicationCommand : IRequest<Result<int>>
    {
        public int ApplicantId { get; set; }
    }

    internal class DeleteJobApplicationCommandHandler : IRequestHandler<DeleteJobApplicationCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteJobApplicationCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteJobApplicationCommandHandler> _localizer;

        public DeleteJobApplicationCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<DeleteJobApplicationCommandHandler> logger,
            IStringLocalizer<DeleteJobApplicationCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteJobApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Attempting to delete job application with Applicant ID {ApplicantId}", request.ApplicantId);
                var jobApplication = await _unitOfWork.Repository<HRMS.Domain.Entities.Recruitment.JobApplication>().GetByIdAsync(request.ApplicantId);

                if (jobApplication == null)
                {
                    _logger.LogWarning("Job application not found for Applicant ID {ApplicantId}", request.ApplicantId);
                    return await Result<int>.FailAsync(_localizer["Job Application Not Found"]);
                }

                await _unitOfWork.Repository<HRMS.Domain.Entities.Recruitment.JobApplication>().DeleteAsync(jobApplication);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken);

                _logger.LogInformation("Successfully deleted job application with Applicant ID {ApplicantId}", request.ApplicantId);
                return await Result<int>.SuccessAsync(request.ApplicantId, _localizer["Job Application Deleted"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting job application with Applicant ID {ApplicantId}", request.ApplicantId);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}