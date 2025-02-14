using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.Delete;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.Delete
{
    public class DeleteJobLocationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteJobLocationCommandHandler : IRequestHandler<DeleteJobLocationCommand, Result<int>>
    {
        private readonly ILogger<DeleteJobLocationCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteJobLocationCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobLocationCommandHandler(ILogger<DeleteJobLocationCommandHandler> logger, IStringLocalizer<DeleteJobLocationCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteJobLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing JobLocation...");
                var dept = await _unitOfWork.Repository<JobLocation>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("JobLocation Deletion started...");
                    await _unitOfWork.Repository<JobLocation>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobLocationCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("JobLocation Deleted Successfully.");
                    return await Result<int>.SuccessAsync("JobLocation Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("JobLocation not Exists.");
                    return await Result<int>.FailAsync("JobLocation does't exists.");
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
