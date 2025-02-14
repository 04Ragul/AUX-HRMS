using Microsoft.AspNetCore.Localization;
using HRMS.Shared.Constants.Application;
using HRMS.Shared.Constants.Localization;
using HRMS.Application.Configurations;
using HRMS.Application.Interfaces.Services;
using HRMS.Web.Api.Hubs;
using HRMS.Web.Api.Middlewares;
using System.Globalization;

namespace HRMS.Web.Api.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                //app.UseWebAssemblyDebugging();
            }

            return app;
        }

        internal static IApplicationBuilder UseForwarding(this IApplicationBuilder app, IConfiguration configuration)
        {
            AppConfiguration config = GetApplicationSettings(configuration);
            if (config.BehindSSLProxy)
            {
                _ = app.UseCors();
                _ = app.UseForwardedHeaders();
            }

            return app;
        }

        internal static void ConfigureSwagger(this IApplicationBuilder app)
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }

        internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                _ = endpoints.MapControllers();
                //endpoints.MapFallbackToFile("index.html");
                _ = endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
            });
        }

        internal static IApplicationBuilder UseRequestLocalizationByCulture(this IApplicationBuilder app)
        {
            CultureInfo[] supportedCultures = LocalizationConstants.SupportedLanguages.Select(l => new CultureInfo(l.Code)).ToArray();
            _ = app.UseRequestLocalization(options =>
            {
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
                options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
                options.ApplyCurrentCultureToResponseHeaders = true;
            });

            _ = app.UseMiddleware<RequestCultureMiddleware>();

            return app;
        }

        internal static IApplicationBuilder Initialize(this IApplicationBuilder app, IConfiguration _configuration)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            IEnumerable<IDatabaseSeeder> initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (IDatabaseSeeder initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }

        private static AppConfiguration GetApplicationSettings(IConfiguration configuration)
        {
            IConfigurationSection applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
            return applicationSettingsConfiguration.Get<AppConfiguration>();
        }
    }
}
