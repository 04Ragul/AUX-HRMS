using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetById;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetAll
{
    public class GetAllJobQuery : IRequest<Result<List<GetAllJobResponse>>>
    {
    }
    internal class GetAllJobQueryHandler : IRequestHandler<GetAllJobQuery, Result<List<GetAllJobResponse>>>
    {
        private readonly IStringLocalizer<GetAllJobQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllJobQueryHandler> _logger;

        public GetAllJobQueryHandler(IStringLocalizer<GetAllJobQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetAllJobQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<List<GetAllJobResponse>>> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.Job, GetAllJobResponse>> expression = e => new GetAllJobResponse
                {
                    Id = e.Id,
                    JobCategoryId = e.JobCategoryId,
                    CompanyId = e.CompanyId,
                    Description = e.Description,
                    JobLocationId = e.JobLocationId,
                    LastApplicationDate = e.LastApplicationDate,
                    NoOfVacancy = e.NoOfVacancy,
                    Salary = e.Salary,
                    Status = e.Status,
                    Title = e.Title,
                    Type = e.Type
                };
                var res = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Job>().Entities.Select(expression).ToListAsync();
                return await Result<List<GetAllJobResponse>>.SuccessAsync(data: res, "Get All jobs success");
            }
            catch (Exception ex)
            {
                return await Result<List<GetAllJobResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
