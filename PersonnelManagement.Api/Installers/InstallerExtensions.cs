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
    }
}
