using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace PersonnelManagement.WebClient.Installers
{
    public static class InstallersExtensions
    {
        public static WebAssemblyHostBuilder InstallServices(this WebAssemblyHostBuilder builder)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                .Where(i => typeof(IInstaller).IsAssignableFrom(i) && !i.IsInterface && !i.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(installer =>
                installer.InstallServices(builder)
            );

            return builder;
        }
    }
}
