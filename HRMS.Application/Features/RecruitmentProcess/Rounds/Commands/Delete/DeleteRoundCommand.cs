using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Rounds.Commands.Delete;
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

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Commands.Delete
{
    public class DeleteRoundCommand:IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteRoundCommandHandler : IRequestHandler<DeleteRoundCommand, Result<int>>
    {
        private readonly ILogger<DeleteRoundCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteRoundCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoundCommandHandler(ILogger<DeleteRoundCommandHandler> logger, IStringLocalizer<DeleteRoundCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteRoundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing Round...");
                var dept = await _unitOfWork.Repository<Round>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("Round Deletion started...");
                    await _unitOfWork.Repository<Round>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllRoundCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("Round Deleted Successfully.");
                    return await Result<int>.SuccessAsync("Round Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("Round not Exists.");
                    return await Result<int>.FailAsync("Round does't exists.");
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
