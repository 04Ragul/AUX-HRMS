using HRMS.Application.Interfaces.Services;

namespace HRMS.Web.Api.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
