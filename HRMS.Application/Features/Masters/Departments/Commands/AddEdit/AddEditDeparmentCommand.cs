using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.Employees.Commands.AddEdit;
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

namespace HRMS.Application.Features.Departments.Commands.AddEdit
{
    public class AddEditDeparmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    internal class AddEditDeparmentCommandHandler : IRequestHandler<AddEditDeparmentCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditDeparmentCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditDeparmentCommandHandler> _logger;
        private readonly IValidator<AddEditDeparmentCommand> _addEditDeparmentCommandValidator;

        public AddEditDeparmentCommandHandler(IStringLocalizer<AddEditDeparmentCommandHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<AddEditDeparmentCommandHandler> logger, IValidator<AddEditDeparmentCommand> addEditDeparmentCommandValidator)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _addEditDeparmentCommandValidator = addEditDeparmentCommandValidator;
        }

        public async Task<Result<int>> Handle(AddEditDeparmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Department Add/Update Validation Started");
                var validationResult = _addEditDeparmentCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Department Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("Depart Mapping to DTO for New Record");
                        var dept = _mapper.Map<Department>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("Department {Name} is Staring Adding to Departments", dept.Name);
                            var res = await _unitOfWork.Repository<Department>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDepartmentCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Department {Name} is Added Successfully", res.Name);
                            return await Result<int>.SuccessAsync(res.Id, _localize["Department Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map Department Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing Department Data...");
                        var dept = await _unitOfWork.Repository<Department>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                            dept.Name = request.Name;
                            dept.Code = request.Code;
                            dept.Description = request.Description;
                            _logger.LogInformation("Updating Department Data...");
                            await _unitOfWork.Repository<Department>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDepartmentCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Department Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localize["Department Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find Department");
                        }
                    }
                }
                else
                {
                    _logger.LogError("Department Validation Failed {0}", validationResult.Errors);
                    return await Result<int>.FailAsync(validationResult!.Errors!.FirstOrDefault()!.ErrorMessage);
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
