using HRMS.Domain.Entities.Audit;
using HRMS.Application.Specifications.Base;

namespace HRMS.Infrastructure.Specifications
{
    public class AuditFilterSpecification : Specification<Audit>
	{
		public AuditFilterSpecification(string userId, string searchString, bool searchInOldValues, bool searchInNewValues)
		{
			Criteria = !string.IsNullOrEmpty(searchString)
				? (p => (p.TableName.Contains(searchString) || (searchInOldValues && p.OldValues.Contains(searchString)) || (searchInNewValues && p.NewValues.Contains(searchString))) && p.UserId == userId)
				: (p => p.UserId == userId);
		}
		public AuditFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => (p.UserId!.ToLower().Contains(searchString.ToLower()) || p.TableName.Contains(searchString) ||  p.OldValues.Contains(searchString)) || p.NewValues.Contains(searchString))
                : (p => true);
        }
    }
}
