using FluentValidation;
using FluentValidation.Validators;
using HRMS.Shared.Utilities.Interfaces.Serialization.Serializers;

namespace HRMS.Shared.Utilities.Validators
{
    public class JsonValidator<T> : PropertyValidator<T, string>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public JsonValidator(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public override string Name => "JsonValidator";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            bool isJson = true;
            value = value.Trim();
            try
            {
                _ = _jsonSerializer.Deserialize<object>(value);
            }
            catch
            {
                isJson = false;
            }
            isJson = (isJson && value.StartsWith("{") && value.EndsWith("}"))
                     || (value.StartsWith("[") && value.EndsWith("]"));

            return isJson;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} must be json string.";
        }
    }
}
