using HRMS.Domain.Entities.Identity;
using HRMS.Application.Specifications.Base;

namespace HRMS.Infrastructure.Specifications
{
    public class UserFilterSpecification : Specification<ApplicationUser>
    {
        public UserFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => p.Name.Contains(searchString)  || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString))
                : (p => true);
        }
    }
}
