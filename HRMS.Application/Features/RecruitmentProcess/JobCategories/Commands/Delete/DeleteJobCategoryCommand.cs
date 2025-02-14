using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Contract;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.Delete
{
    public class DeleteJobCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteJobCategoryCommandHandler : IRequestHandler<DeleteJobCategoryCommand, Result<int>>
    {
        private readonly ILogger<DeleteJobCategoryCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteJobCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobCategoryCommandHandler(ILogger<DeleteJobCategoryCommandHandler> logger, IStringLocalizer<DeleteJobCategoryCommandHandler> localizer, IUnitOfWork<int> unitOfWork,
              IMapper mapper)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteJobCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing JobCategory...");
                var dept = await _unitOfWork.Repository<JobCategory>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("JobCategory Deletion started...");
                    await _unitOfWork.Repository<JobCategory>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobCategoryCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("JobCategory Deleted Successfully.");
                    return await Result<int>.SuccessAsync("JobCategory Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("JobCategory not Exists.");
                    return await Result<int>.FailAsync("JobCategory does't exists.");
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
