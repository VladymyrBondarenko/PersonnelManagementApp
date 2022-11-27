using Serilog;

namespace PersonnelManagement.Api.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                .Where(i => typeof(IInstaller).IsAssignableFrom(i) && !i.IsInterface && !i.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(installer =>
                installer.InstallServices(configuration, services)
            );
        }

        public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((hostContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration)
                    .Enrich.FromLogContext()
                    .ReadFrom.Services(services);
            });
        }
    }
}
