using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Features.Organisations;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Branches.Commands.Delete
{
    public class DeleteBranchCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ILogger<DeleteBranchCommandHandler> _logger;

        public DeleteBranchCommandHandler(IUnitOfWork<int> unitOfWork, ILogger<DeleteBranchCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Branch for Deletion: {Id}", request.Id);
                var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(request.Id);
                if (branch == null)
                {
                    _logger.LogError("Branch not found: {Id}", request.Id);
                    return await Result<int>.FailAsync("Branch does not exist.");
                }

                _logger.LogInformation("Deleting Branch: {Id}", request.Id);
                await _unitOfWork.Repository<Branch>().DeleteAsync(branch);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBranchCacheKey);
                _logger.LogInformation("Branch Deleted Successfully: {Id}", request.Id);

                return await Result<int>.SuccessAsync(request.Id, "Branch Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting Branch: {Id}", request.Id);
                return await Result<int>.FailAsync(ex.Message);
            }
        }
    }
}