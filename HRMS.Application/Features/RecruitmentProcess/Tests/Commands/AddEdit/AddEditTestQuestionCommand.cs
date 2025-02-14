using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Tests.Commands.AddEdit
{
    public class AddEditTestQuestionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public bool IsMultiSelect { get; set; }
        public string Ans { get; set; }
        public int TestId { get; set; }
        public int OptionId { get; set; }
        public string Question { get; set; }
    }
    internal class AddEditTestQuestionCommandHandler : IRequestHandler<AddEditTestQuestionCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddEditTestQuestionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
