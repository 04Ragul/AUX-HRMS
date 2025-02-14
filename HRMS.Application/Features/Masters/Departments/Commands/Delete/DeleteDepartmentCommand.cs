using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features;
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

namespace HRMS.Application.Features.Departments.Commands.Delete
{
    public class DeleteDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteDepartmentCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;

        public DeleteDepartmentCommandHandler(IStringLocalizer<DeleteDepartmentCommandHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<DeleteDepartmentCommandHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing Department...");
                var dept = await _unitOfWork.Repository<Department>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("Department Deletion started...");
                    await _unitOfWork.Repository<Department>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDepartmentCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("Department Deleted Successfully.");
                    return await Result<int>.SuccessAsync("Department Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("Department not Exists.");
                    return await Result<int>.FailAsync("Department does't exists.");
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
