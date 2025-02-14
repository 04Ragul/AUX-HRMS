using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using HRMS.Application.Exceptions;
using HRMS.Application.Interfaces.Services;
using HRMS.Application.Interfaces.Services.Identity;
using HRMS.Infrastructure.Contexts;
using HRMS.Shared.Constants.Role;
using HRMS.Shared.Wrapper;
using HRMS.Domain.Entities.Chat;
using HRMS.Domain.Interfaces.Chat;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<ChatService> _localizer;

        public ChatService(
            ApplicationDbContext context,
            IMapper mapper,
            IUserService userService,
            IStringLocalizer<ChatService> localizer)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _localizer = localizer;
        }

        public async Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(int userId, int contactId)
        {
            IResult<UserResponse> response = await _userService.GetAsync(userId.ToString());
            if (response.Succeeded)
            {
                UserResponse user = response.Data;
                List<ChatHistoryResponse> query = await _context.ChatHistories
                    .Where(h => (h.FromUserId == user.Id && h.ToUserId == contactId) || (h.FromUserId == contactId && h.ToUserId == user.Id))
                    .OrderBy(a => a.CreatedDate)
                    .Include(a => a.FromUser)
                    .Include(a => a.ToUser)
                    .Select(x => new ChatHistoryResponse
                    {
                        FromUserId = x.FromUserId,
                        FromUserFullName = $"{x.FromUser.Name} ",
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        ToUserId = x.ToUserId,
                        ToUserFullName = $"{x.ToUser.Name}",
                        ToUserImageURL = x.ToUser.ProfilePictureDataUrl,
                        FromUserImageURL = x.FromUser.ProfilePictureDataUrl
                    }).AsNoTracking().ToListAsync();
                return await Result<IEnumerable<ChatHistoryResponse>>.SuccessAsync(query);
            }
            else
            {
                throw new ApiException(_localizer["User Not Found!"]);
            }
        }

        public async Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(int userId)
        {
            IResult<UserRolesResponse> userRoles = await _userService.GetRolesAsync(userId.ToString());
            bool userIsAdmin = userRoles.Data?.UserRoles?.Any(x => x.Selected && x.RoleName == RoleConstants.AdministratorRole) == true;
            List<ApplicationUser> allUsers = await _context.Users.Where(user => user.Id != userId && (userIsAdmin || (user.IsActive && user.EmailConfirmed))).AsNoTracking().ToListAsync();
            IEnumerable<ChatUserResponse> chatUsers = _mapper.Map<IEnumerable<ChatUserResponse>>(allUsers);
            return await Result<IEnumerable<ChatUserResponse>>.SuccessAsync(chatUsers);
        }

        public async Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message)
        {
            message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
            _ = await _context.ChatHistories.AddAsync(_mapper.Map<ChatHistory<ApplicationUser>>(message));
            _ = await _context.SaveChangesAsync();
            return await Result.SuccessAsync();
        }
    }
}
