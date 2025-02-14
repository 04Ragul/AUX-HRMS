
using HRMS.Domain.Interfaces.Chat;
using HRMS.Domain.Entities.Chat;
using HRMS.Shared.Wrapper;
using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(int userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(int userId, int contactId);
    }
}
