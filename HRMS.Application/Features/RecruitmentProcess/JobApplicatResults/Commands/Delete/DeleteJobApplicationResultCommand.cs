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

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatResults.Commands.Delete
{
    public class DeleteJobApplicationResultCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteJobApplicationResultCommandHandler : IRequestHandler<DeleteJobApplicationResultCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteJobApplicationResultCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteJobApplicationResultCommandHandler> _logger;

        public DeleteJobApplicationResultCommandHandler(
            IStringLocalizer<DeleteJobApplicationResultCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<DeleteJobApplicationResultCommandHandler> logger)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(DeleteJobApplicationResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching job application result with ID {Id}", request.Id);
                var jobApplicationResult = await _unitOfWork.Repository<JobApplicationResult>().GetByIdAsync(request.Id).ConfigureAwait(false);

                if (jobApplicationResult == null)
                {
                    _logger.LogError("Job application result with ID {Id} does not exist", request.Id);
                    return await Result<int>.FailAsync(_localizer["Job Application Result does not exist."]);
                }

                _logger.LogInformation("Deleting job application result with ID {Id}", request.Id);
                await _unitOfWork.Repository<JobApplicationResult>().DeleteAsync(jobApplicationResult).ConfigureAwait(false);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationResultsCacheKey).ConfigureAwait(false);

                _logger.LogInformation("Job application result deleted successfully");
                return await Result<int>.SuccessAsync(request.Id, _localizer["Job Application Result Deleted Successfully."]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting job application result with ID {Id}", request.Id);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}