using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using PersonnelManagement.WebClient.Infrastructure.Authentication;
using Refit;

namespace PersonnelManagement.WebClient.Installers
{
    public class BaseInstaller : IInstaller
    {
        public void InstallServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices(opt =>
            {
                opt.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            });
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services
                .AddScoped<AuthStateProvider>()
                .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                .AddTransient<AuthenticationHeaderHandler>();
        }
    }
}
