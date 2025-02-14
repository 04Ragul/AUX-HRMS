using HRMS.Application.Specifications.Base;
using HRMS.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infra.Infrastructure.Specifications
{
	public class RoleFilterSpecification : Specification<ApplicationRole>
	{
		public RoleFilterSpecification(string searchString)
		{
			Criteria = !string.IsNullOrEmpty(searchString)
				? (p => p.Name.Contains(searchString) || p.Description.Contains(searchString))
				: (p => true);
		}
	}
}
