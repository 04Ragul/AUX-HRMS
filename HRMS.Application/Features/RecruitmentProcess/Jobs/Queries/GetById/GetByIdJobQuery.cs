using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetPaged;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetById
{
    public class GetByIdJobQuery : IRequest<Result<GetByIdJobResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobQueryHandler : IRequestHandler<GetByIdJobQuery, Result<GetByIdJobResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobQueryHandler> _logger;

        public GetByIdJobQueryHandler(IStringLocalizer<GetByIdJobQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobResponse>> Handle(GetByIdJobQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var job = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Job>().Entities.Where(x => x.Id == request.Id)
                                   .Select(x => new GetByIdJobResponse()
                                   {
                                       Id = x.Id,
                                       Description = x.Description,
                                       CompanyId = x.CompanyId,
                                       JobCategoryId = x.JobCategoryId,
                                       JobLocationId = x.JobLocationId,
                                       LastApplicationDate = x.LastApplicationDate,
                                       NoOfVacancy = x.NoOfVacancy,
                                       Title = x.Title,
                                       Salary = x.Salary,
                                       Status = x.Status,
                                       Type = x.Type
                                   })
                                   .FirstOrDefaultAsync();
                if (job == null)
                {
                    return await Result<GetByIdJobResponse>.FailAsync("Jobs Not Available");
                }
                return await Result<GetByIdJobResponse>.SuccessAsync(data: job, "Get Job Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Result<GetByIdJobResponse>.FailAsync(ex.Message);
            }
        }
    }
}
