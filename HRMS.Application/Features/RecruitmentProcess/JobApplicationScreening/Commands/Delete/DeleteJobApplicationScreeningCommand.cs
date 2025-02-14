using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HRMS.Application.Interfaces.Repositories;
using Microsoft.Extensions.Localization;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicationScreening.Commands.Delete
{
    public class DeleteJobApplicationScreeningCommand : IRequest<Result<int>>
    {
        public int ScreeningId { get; set; }
    }

    internal class DeleteJobApplicationScreeningCommandHandler : IRequestHandler<DeleteJobApplicationScreeningCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteJobApplicationScreeningCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteJobApplicationScreeningCommandHandler> _localizer;

        public DeleteJobApplicationScreeningCommandHandler(
            IUnitOfWork<int> unitOfWork,
            ILogger<DeleteJobApplicationScreeningCommandHandler> logger,
            IStringLocalizer<DeleteJobApplicationScreeningCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteJobApplicationScreeningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Attempting to delete job application screening with Screening ID {ScreeningId}", request.ScreeningId);
                var screening = await _unitOfWork.Repository<HRMS.Domain.Entities.Recruitment.JobApplicationScreening>().GetByIdAsync(request.ScreeningId);

                if (screening == null)
                {
                    _logger.LogWarning("Job application screening not found for Screening ID {ScreeningId}", request.ScreeningId);
                    return await Result<int>.FailAsync(_localizer["Job Application Screening Not Found"]);
                }

                await _unitOfWork.Repository<HRMS.Domain.Entities.Recruitment.JobApplicationScreening>().DeleteAsync(screening);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken);

                _logger.LogInformation("Successfully deleted job application screening with Screening ID {ScreeningId}", request.ScreeningId);
                return await Result<int>.SuccessAsync(request.ScreeningId, _localizer["Job Application Screening Deleted"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting job application screening with Screening ID {ScreeningId}", request.ScreeningId);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}