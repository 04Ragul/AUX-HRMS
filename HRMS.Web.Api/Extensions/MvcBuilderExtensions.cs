using FluentValidation.AspNetCore;
using HRMS.Application.Configurations;

namespace HRMS.Web.Api.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            _ = builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }


    }
}
