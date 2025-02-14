using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Commands.AddEdit;
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

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Commands.Delete
{
    public class DeleteJobCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result<int>>
    {
        private readonly ILogger<DeleteJobCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteJobCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobCommandHandler(ILogger<DeleteJobCommandHandler> logger, IStringLocalizer<DeleteJobCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing Job...");
                var dept = await _unitOfWork.Repository<Job>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("Job Deletion started...");
                    await _unitOfWork.Repository<Job>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("Job Deleted Successfully.");
                    return await Result<int>.SuccessAsync("Job Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("Job not Exists.");
                    return await Result<int>.FailAsync("Job does't exists.");
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

