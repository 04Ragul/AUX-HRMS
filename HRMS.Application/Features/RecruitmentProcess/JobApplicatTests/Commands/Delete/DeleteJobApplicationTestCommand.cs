using AutoMapper;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Recruitment;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatTests.Commands.Delete
{
    public class DeleteJobApplicationTestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteJobApplicationTestCommandHandler : IRequestHandler<DeleteJobApplicationTestCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteJobApplicationTestCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteJobApplicationTestCommandHandler> _logger;

        public DeleteJobApplicationTestCommandHandler(
            IStringLocalizer<DeleteJobApplicationTestCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            ILogger<DeleteJobApplicationTestCommandHandler> logger)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(DeleteJobApplicationTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Job Application Test for deletion, ID: {Id}", request.Id);
                var jobApplicationTest = await _unitOfWork.Repository<JobApplicationTest>().GetByIdAsync(request.Id);

                if (jobApplicationTest == null)
                {
                    _logger.LogError("Job Application Test not found for ID: {Id}", request.Id);
                    return await Result<int>.FailAsync(_localizer["Job Application Test not found."]);
                }

                _logger.LogInformation("Deleting Job Application Test ID: {Id}", request.Id);
                await _unitOfWork.Repository<JobApplicationTest>().DeleteAsync(jobApplicationTest);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationTestsCacheKey);

                _logger.LogInformation("Job Application Test deleted successfully, ID: {Id}", request.Id);
                return await Result<int>.SuccessAsync(request.Id, _localizer["Job Application Test deleted successfully."]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Job Application Test, ID: {Id}", request.Id);
                return await Result<int>.FailAsync("An error occurred while deleting the job application test.");
            }
        }
    }
}