using HRMS.Shared.Utilities.Interfaces.Serialization.Options;
using System.Text.Json;

namespace HRMS.Shared.Utilities.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}
