using AutoMapper;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteEmployeeCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandler(
            IStringLocalizer<DeleteEmployeeCommandHandler> localize,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching employee for deletion...");

                var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (employee == null)
                {
                    _logger.LogError("Employee does not exist.");
                    return await Result<int>.FailAsync("Employee does not exist.");
                }

                _logger.LogInformation("Deleting employee {FullName}...", employee.FullName);
                await _unitOfWork.Repository<Employee>().DeleteAsync(employee).ConfigureAwait(false);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllEmployeesCacheKey).ConfigureAwait(false);

                _logger.LogInformation("Employee {FullName} deleted successfully.", employee.FullName);
                return await Result<int>.SuccessAsync(employee.Id, "Employee Deleted Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee.");
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}
