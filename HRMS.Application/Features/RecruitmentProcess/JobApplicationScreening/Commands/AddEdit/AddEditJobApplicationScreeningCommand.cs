using AutoMapper;
using FluentValidation;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities.Recruitment;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicationScreening.Commands.AddEdit
{
    public class AddEditJobApplicationScreeningCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int JobId { get; set; }
        public string ScreeningStatus { get; set; }
        public string Comments { get; set; }
    }

    internal class AddEditJobApplicationScreeningCommandHandler : IRequestHandler<AddEditJobApplicationScreeningCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddEditJobApplicationScreeningCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEditJobApplicationScreeningCommandHandler> _logger;
        private readonly IValidator<AddEditJobApplicationScreeningCommand> _validator;

        public AddEditJobApplicationScreeningCommandHandler(
            IStringLocalizer<AddEditJobApplicationScreeningCommandHandler> localizer,
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            ILogger<AddEditJobApplicationScreeningCommandHandler> logger,
            IValidator<AddEditJobApplicationScreeningCommand> validator)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(AddEditJobApplicationScreeningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Job Application Screening Validation Started");
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation Failed: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return await Result<int>.FailAsync(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation error");
                }

                _logger.LogInformation("Validation Succeeded");
                if (request.Id == 0)
                {
                    _logger.LogInformation("Mapping to DTO for new screening record");
                    var screening = _mapper.Map<JobApplicationScreening>(request);

                    _logger.LogInformation("Adding Job Application Screening for Applicant ID {ApplicantId}", screening.ApplicantId);
                    var result = await _unitOfWork.Repository<JobApplicationScreening>().AddAsync(screening);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationScreeningCacheKey);

                    _logger.LogInformation("Job Application Screening added successfully");
                    return await Result<int>.SuccessAsync(result.Id, _localizer["Screening Saved"]);
                }
                else
                {
                    _logger.LogInformation("Fetching existing screening record for ID {Id}", request.Id);
                    var screening = await _unitOfWork.Repository<JobApplicationScreening>().GetByIdAsync(request.Id);

                    if (screening == null)
                    {
                        _logger.LogError("Screening not found for ID {Id}", request.Id);
                        return await Result<int>.FailAsync("Screening not found");
                    }

                    screening.ScreeningStatus = request.ScreeningStatus;
                    screening.Comments = request.Comments;

                    _logger.LogInformation("Updating screening record...");
                    await _unitOfWork.Repository<JobApplicationScreening>().UpdateAsync(screening);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllJobApplicationScreeningCacheKey);

                    _logger.LogInformation("Screening record updated successfully");
                    return await Result<int>.SuccessAsync(screening.Id, _localizer["Screening Updated"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing job application screening: {Message}", ex.Message);
                return await Result<int>.FailAsync("An error occurred while processing your request.");
            }
        }
    }
}