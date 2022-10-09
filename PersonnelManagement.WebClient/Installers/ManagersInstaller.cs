using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PersonnelManagement.Sdk.Departments;
using PersonnelManagement.WebClient.Infrastructure.Managers.Departments;
using PersonnelManagement.WebClient.Options;
using Refit;

namespace PersonnelManagement.WebClient.Installers
{
    public class ManagersInstaller : IInstaller
    {
        public void InstallServices(WebAssemblyHostBuilder builder)
        {
            var managersOptions = new ManagersApiOptions();
            builder.Configuration.GetSection(nameof(ManagersApiOptions)).Bind(managersOptions);
            builder.Services.AddSingleton(managersOptions);

            builder.Services.AddHttpClient(managersOptions.ClientName, config =>
            {
                config.BaseAddress = new Uri(managersOptions.ApiBaseUrl);
            });

            builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
        }
    }
}
