using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetById
{
    public class GetByIdRoundQuery : IRequest<Result<GetByIdRoundResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdRoundQueryHandler : IRequestHandler<GetByIdRoundQuery, Result<GetByIdRoundResponse>>
    {
        private readonly IStringLocalizer<GetByIdRoundQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdRoundQueryHandler> _logger;

        public GetByIdRoundQueryHandler(IStringLocalizer<GetByIdRoundQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdRoundQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdRoundResponse>> Handle(GetByIdRoundQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Round = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Round>().Entities.Where(x => x.Id == request.Id)
                                   .Select(x => new GetByIdRoundResponse()
                                   {
                                       Id = x.Id,
                                       Name = x.Name,
                                       JobCategoryId = x.JobCategoryId
                                   })
                                   .FirstOrDefaultAsync();
                if (Round == null)
                {
                    return await Result<GetByIdRoundResponse>.FailAsync("Rounds Not Available");
                }
                return await Result<GetByIdRoundResponse>.SuccessAsync(data: Round, "Get Round Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Result<GetByIdRoundResponse>.FailAsync(ex.Message);
            }
        }
    }
}
