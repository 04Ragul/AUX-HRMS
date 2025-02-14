using HRMS.Shared.Constants.Localization;
using HRMS.Shared.Settings;

namespace HRMS.Web.Api.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}
