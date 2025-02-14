using AutoMapper;
using HRMS.Application.Features.Organisations.Locations.Commands.Delete;
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

namespace HRMS.Application.Features.Organisations.Locations.Commands.Delete
{
    internal class DeleteOrganisationLocationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteOrganisationLocationCommandHandler : IRequestHandler<DeleteOrganisationLocationCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteOrganisationLocationCommandHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrganisationLocationCommandHandler> _logger;

        public DeleteOrganisationLocationCommandHandler(IStringLocalizer<DeleteOrganisationLocationCommandHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<DeleteOrganisationLocationCommandHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<int>> Handle(DeleteOrganisationLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Existing Organisation Location...");
                var dept = await _unitOfWork.Repository<Location>().GetByIdAsync(request.Id).ConfigureAwait(false);
                if (dept == null)
                {
                    _logger.LogInformation("Organisation Location Deletion started...");
                    await _unitOfWork.Repository<Location>().DeleteAsync(dept!).ConfigureAwait(false);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllOrganisationLocationCacheKey).ConfigureAwait(false);
                    _logger.LogInformation("Organisation Location Deleted Successfully.");
                    return await Result<int>.SuccessAsync("Organisation Location Deleted Successfully.");
                }
                else
                {
                    _logger.LogError("Organisation Location not Exists.");
                    return await Result<int>.FailAsync("Organisation Location does't exists.");
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
