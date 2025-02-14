using AutoMapper;
using FluentValidation;
using HRMS.Application.Features.Organisations.Locations.Commands.AddEdit;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features.Organisations;
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

namespace HRMS.Application.Features.Organisations.Locations.Commands.AddEdit
{
    public class AddEditOrganisationLocationCommand:IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    internal class AddEditOrganisationLocationCommandHandler : IRequestHandler<AddEditOrganisationLocationCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditOrganisationLocationCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditOrganisationLocationCommandHandler> _logger;
        private readonly IValidator<AddEditOrganisationLocationCommand> _addEditOrganisationLocationCommandValidator;

        public AddEditOrganisationLocationCommandHandler(IStringLocalizer<AddEditOrganisationLocationCommandHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<AddEditOrganisationLocationCommandHandler> logger, IValidator<AddEditOrganisationLocationCommand> addEditOrganisationLocationCommandValidator)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _addEditOrganisationLocationCommandValidator = addEditOrganisationLocationCommandValidator;
        }

        public async Task<Result<int>> Handle(AddEditOrganisationLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Organisation Location Add/Update Validation Started");
                var validationResult = _addEditOrganisationLocationCommandValidator.Validate(request);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Organisation Location Validation Succeed...");
                    if (request.Id == 0)
                    {
                        _logger.LogInformation("Depart Mapping to DTO for New Record");
                        var dept = _mapper.Map<Location>(request);
                        if (dept != null)
                        {
                            _logger.LogInformation("Organisation Location {Name} is Staring Adding to Organisation Locations", dept.Name);
                            var res = await _unitOfWork.Repository<Location>().AddAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllOrganisationLocationCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("Organisation Location {Name} is Added Successfully", res.Name);
                            return await Result<int>.SuccessAsync(res.Id, _localize["Organisation Location Saved"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to Map Organisation Location Data");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Getting Existing Organisation Location Data...");
                        var dept = await _unitOfWork.Repository<Location>().GetByIdAsync(request.Id);
                        if (dept != null)
                        {
                            dept.Name = request.Name;
                            dept.Latitude = request.Latitude;
                            dept.Longitude = request.Longitude;
                            _logger.LogInformation("Updating OrganisationLocation Data...");
                            await _unitOfWork.Repository<Location>().UpdateAsync(dept!).ConfigureAwait(false);
                            await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllOrganisationLocationCacheKey).ConfigureAwait(false);
                            _logger.LogInformation("OrganisationLocation Updated.");
                            return await Result<int>.SuccessAsync(dept.Id, _localize["OrganisationLocation Updated"]);
                        }
                        else
                        {
                            return await Result<int>.FailAsync("Unable to find OrganisationLocation");
                        }
                    }
                }
                else
                {
                    _logger.LogError("OrganisationLocation Validation Failed {0}", validationResult.Errors);
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
