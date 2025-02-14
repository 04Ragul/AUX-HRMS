using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
