namespace HRMS.Shared.Utilities.Requests.Mail
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public bool IsRelayServer { get; set; }
    }
}
