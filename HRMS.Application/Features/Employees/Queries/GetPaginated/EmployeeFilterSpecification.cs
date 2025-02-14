namespace HRMS.Application.Features.Employees.Queries.GetPaginated
{
    internal class EmployeeFilterSpecification
    {
        private string searchString;

        public EmployeeFilterSpecification(string searchString)
        {
            this.searchString = searchString;
        }
    }
}