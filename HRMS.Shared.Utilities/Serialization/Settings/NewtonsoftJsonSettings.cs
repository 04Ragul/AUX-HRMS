using Newtonsoft.Json;
using HRMS.Shared.Utilities.Interfaces.Serialization.Settings;

namespace HRMS.Shared.Utilities.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}
