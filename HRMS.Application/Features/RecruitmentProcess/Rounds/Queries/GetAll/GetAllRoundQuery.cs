using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetAll;
using HRMS.Application.Interfaces.Repositories;
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

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetAll
{
    public class GetAllRoundQuery : IRequest<Result<List<GetAllRoundResponse>>>
    {
    }
    internal class GetAllRoundQueryHandler : IRequestHandler<GetAllRoundQuery, Result<List<GetAllRoundResponse>>>
    {
        private readonly IStringLocalizer<GetAllRoundQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoundQueryHandler> _logger;

        public GetAllRoundQueryHandler(IStringLocalizer<GetAllRoundQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetAllRoundQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<List<GetAllRoundResponse>>> Handle(GetAllRoundQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.Round, GetAllRoundResponse>> expression = e => new GetAllRoundResponse
                {
                    Id = e.Id,
                    JobCategoryId = e.JobCategoryId,
                    Name = e.Name
                };
                var res = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Round>().Entities.Select(expression).ToListAsync();
                return await Result<List<GetAllRoundResponse>>.SuccessAsync(data: res, "Get All Rounds success");
            }
            catch (Exception ex)
            {
                return await Result<List<GetAllRoundResponse>>.FailAsync(ex.Message);
            }
        }
    }
}
