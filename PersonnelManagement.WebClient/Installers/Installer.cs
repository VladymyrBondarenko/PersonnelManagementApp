using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace PersonnelManagement.WebClient.Installers
{
    public class Installer : IInstaller
    {
        public void InstallServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices(opt =>
            {
                opt.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            });
            builder.Services.AddAutoMapper(typeof(Program));
        }
    }
}
