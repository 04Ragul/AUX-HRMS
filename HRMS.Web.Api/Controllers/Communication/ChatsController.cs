﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Shared.Constants.Permission;
using HRMS.Domain.Interfaces.Chat;
using HRMS.Application.Interfaces.Services;
using HRMS.Domain.Entities.Chat;

namespace HRMS.Web.Api.Controllers.Communication
{
   // [Authorize(Policy = Permissions.Communication.Chat)]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IChatService _chatService;

        public ChatsController(ICurrentUserService currentUserService, IChatService chatService)
        {
            _currentUserService = currentUserService;
            _chatService = chatService;
        }

        /// <summary>
        /// Get user wise chat history
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns>Status 200 OK</returns>
        //Get user wise chat history
        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetChatHistoryAsync(int contactId)
        {
            return Ok(await _chatService.GetChatHistoryAsync(_currentUserService.UserId, contactId));
        }
        /// <summary>
        /// get available users
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //get available users - sorted by date of last message if exists
        [HttpGet("users")]
        public async Task<IActionResult> GetChatUsersAsync()
        {
            return Ok(await _chatService.GetChatUsersAsync(_currentUserService.UserId));
        }

        /// <summary>
        /// Save Chat Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Status 200 OK</returns>
        //save chat message
        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatHistory<IChatUser> message)
        {
            message.FromUserId = _currentUserService.UserId;
            message.ToUserId = message.ToUserId;
            message.CreatedDate = DateTime.Now;
            return Ok(await _chatService.SaveMessageAsync(message));
        }
    }
}
