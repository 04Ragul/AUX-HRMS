namespace HRMS.Shared.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
            public const string SendUpdateDashboard = "UpdateDashboardAsync";
            public const string ReceiveUpdateDashboard = "UpdateDashboard";
            public const string SendRegenerateTokens = "RegenerateTokensAsync";
            public const string ReceiveRegenerateTokens = "RegenerateTokens";
            public const string ReceiveChatNotification = "ReceiveChatNotification";
            public const string SendChatNotification = "ChatNotificationAsync";
            public const string ReceiveMessage = "ReceiveMessage";
            public const string SendMessage = "SendMessageAsync";

            public const string OnConnect = "OnConnectAsync";
            public const string ConnectUser = "ConnectUser";
            public const string OnDisconnect = "OnDisconnectAsync";
            public const string DisconnectUser = "DisconnectUser";
            public const string OnChangeRolePermissions = "OnChangeRolePermissions";
            public const string LogoutUsersByRole = "LogoutUsersByRole";

            public const string PingRequest = "PingRequestAsync";
            public const string PingResponse = "PingResponseAsync";

        }
        public static class Cache
        {
            public const string GetAllDepartmentCacheKey = "all-departments";

            public const string GetAllJobCategoryCacheKey = "all-job-categories";
            public const string GetAllJobLocationCacheKey="all-job-loctions";

            public const string GetAllRoundCacheKey = "all-rounds";

            public const string GetAllOrganisationLocationCacheKey = "organisation-locations";

            public const string GetAllJobCacheKey = "all-jobs";

            public static string[] GetAllEmployeesCacheKey { get; set; }
        }

        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string OctetStream = "application/octet-stream";
        }
    }
}
