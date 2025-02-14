using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features;
using HRMS.Domain.Entities.Features.Organisations;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Branches.Commands.AddEdit
{
    public class AddEditBranchCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? LandLine { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int LocationId { get; set; }
        public int OrganisationId { get; set; }
    }

    internal class AddEditBranchCommandHandler : IRequestHandler<AddEditBranchCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditBranchCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditBranchCommandHandler> _logger;
        private readonly IValidator<AddEditBranchCommand> _validator;

        public AddEditBranchCommandHandler(
            IStringLocalizer<AddEditBranchCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<AddEditBranchCommandHandler> logger,
            IValidator<AddEditBranchCommand> validator)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(AddEditBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Branch Add/Update Validation Started");
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Branch Validation Failed: {0}", validationResult.Errors);
                    return await Result<int>.FailAsync(validationResult.Errors.First().ErrorMessage);
                }

                if (request.Id == 0)
                {
                    _logger.LogInformation("Mapping Branch DTO for New Record");
                    var branch = _mapper.Map<Branch>(request);
                    if (branch == null)
                        return await Result<int>.FailAsync("Unable to Map Branch Data");

                    _logger.LogInformation("Adding Branch: {Name}", branch.Name);
                    var res = await _unitOfWork.Repository<Branch>().AddAsync(branch);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBranchCacheKey);
                    _logger.LogInformation("Branch {Name} Added Successfully", res.Name);
                    return await Result<int>.SuccessAsync(res.Id, _localizer["Branch Saved"]);
                }
                else
                {
                    _logger.LogInformation("Fetching Existing Branch Data");
                    var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(request.Id);
                    if (branch == null)
                        return await Result<int>.FailAsync("Unable to find Branch");

                    branch.Name = request.Name;
                    branch.Mobile = request.Mobile;
                    branch.LandLine = request.LandLine;
                    branch.Description = request.Description;
                    branch.AddressLine1 = request.AddressLine1;
                    branch.AddressLine2 = request.AddressLine2;
                    branch.ZipCode = request.ZipCode;
                    branch.State = request.State;
                    branch.City = request.City;
                    branch.LocationId = request.LocationId;
                    branch.OrganisationId = request.OrganisationId;

                    _logger.LogInformation("Updating Branch Data");
                    await _unitOfWork.Repository<Branch>().UpdateAsync(branch);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBranchCacheKey);
                    _logger.LogInformation("Branch Updated Successfully");
                    return await Result<int>.SuccessAsync(branch.Id, _localizer["Branch Updated"]);
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