using HRMS.Shared.Utilities.Interfaces.Common;
namespace HRMS.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        int UserId { get; }
        string UserName { get; }
        string IpAddress { get; }
    }
}
