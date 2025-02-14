using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Recruitment;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicationResults.Commands.AddEdit
{
    public class AddEditJobApplicationResultCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int JobId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }

    internal class AddEditJobApplicationResultCommandHandler : IRequestHandler<AddEditJobApplicationResultCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditJobApplicationResultCommandHandler> _logger;
        private readonly IStringLocalizer<AddEditJobApplicationResultCommandHandler> _localizer;
        private readonly IValidator<AddEditJobApplicationResultCommand> _validator;

        public AddEditJobApplicationResultCommandHandler(
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<AddEditJobApplicationResultCommandHandler> logger,
            IStringLocalizer<AddEditJobApplicationResultCommandHandler> localizer,
            IValidator<AddEditJobApplicationResultCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _localizer = localizer;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(AddEditJobApplicationResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating Job Application Result");
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed: {Errors}", string.Join(", ", validationResult.Errors));
                    return await Result<int>.FailAsync(validationResult.Errors[0].ErrorMessage);
                }

                if (request.Id == 0)
                {
                    _logger.LogInformation("Adding new Job Application Result");
                    var result = _mapper.Map<JobApplicationResult>(request);
                    await _unitOfWork.Repository<JobApplicationResult>().AddAsync(result);
                }
                else
                {
                    _logger.LogInformation("Updating existing Job Application Result with ID {Id}", request.Id);
                    var existingResult = await _unitOfWork.Repository<JobApplicationResult>().GetByIdAsync(request.Id);
                    if (existingResult == null)
                    {
                        _logger.LogError("Job Application Result not found");
                        return await Result<int>.FailAsync(_localizer["Job Application Result Not Found"]);
                    }

                    existingResult.Status = request.Status;
                    existingResult.Remarks = request.Remarks;
                    await _unitOfWork.Repository<JobApplicationResult>().UpdateAsync(existingResult);
                }

                await _unitOfWork.CommitAndRemoveCache(cancellationToken);
                _logger.LogInformation("Job Application Result processed successfully");
                return await Result<int>.SuccessAsync(request.Id, _localizer["Job Application Result Processed"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Job Application Result");
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}