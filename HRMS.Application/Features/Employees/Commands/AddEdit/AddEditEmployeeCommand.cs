using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services.Identity;
using HRMS.Domain.Entities.Features;
using HRMS.Domain.Entities.Identity;
using HRMS.Domain.Enums;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Employees.Commands.AddEdit
{
    public class AddEditEmployeeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name => FullName;
        public GenderType Gender { get; set; }
        public string EmployeeID { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime DoB { get; set; }
        public DateTime ReleavingDate { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Orgin { get; set; }
    }
    internal class AddEditEmployeeCommandHandler : IRequestHandler<AddEditEmployeeCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditEmployeeCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditEmployeeCommandHandler> _logger;
        private readonly IUserService _userService;
        private readonly IValidator<AddEditEmployeeCommand> _validator;

        public AddEditEmployeeCommandHandler(IStringLocalizer<AddEditEmployeeCommandHandler> localize, IMapper mapper, IUnitOfWork<int> unitOfWork, ILogger<AddEditEmployeeCommandHandler> logger, IUserService userService, IValidator<AddEditEmployeeCommand> validator)
        {
            _localize = localize;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userService = userService;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(AddEditEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Employee Add/Update Validation Started");
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Employee Validation Failed {0}", validationResult.Errors);
                    return await Result<int>.FailAsync(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
                }

                if (request.Id == 0)
                {
                    var employee = _mapper.Map<Employee>(request);
                    var user = _mapper.Map<RegisterRequest>(request);
                    var res = await _userService.RegisterAsync(user, request.Orgin);
                    if (!res.Succeeded)
                    {
                        return await Result<int>.FailAsync(res.Messages.FirstOrDefault());
                    }

                    _logger.LogInformation("Adding new employee: {FullName}", employee.FullName);
                    var addedEmployee = await _unitOfWork.Repository<Employee>().AddAsync(employee);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, "GetAllEmployeesCacheKey");

                    _logger.LogInformation("Employee {FullName} added successfully", addedEmployee.FullName);
                    return await Result<int>.SuccessAsync(addedEmployee.Id, _localize["Employee Saved"]);
                }
                else
                {
                    _logger.LogInformation("Fetching existing employee data...");
                    var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);
                    if (employee == null) return await Result<int>.FailAsync("Unable to find Employee");

                    _mapper.Map(request, employee);

                    _logger.LogInformation("Updating employee data...");
                    await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, "GetAllEmployeesCacheKey");

                    _logger.LogInformation("Employee updated successfully.");
                    return await Result<int>.SuccessAsync(employee.Id, _localize["Employee Updated"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddEditEmployeeCommandHandler");
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}
