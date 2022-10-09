using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace PersonnelManagement.WebClient.Installers
{
    public interface IInstaller
    {
        void InstallServices(WebAssemblyHostBuilder builder);
    }
}
