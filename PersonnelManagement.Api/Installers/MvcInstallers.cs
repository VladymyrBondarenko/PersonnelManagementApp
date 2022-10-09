using PersonnelManagement.Server.Services;

namespace PersonnelManagement.Api.Installers
{
    public class MvcInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();

            services.AddScoped<IUriService, UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                return new UriService($"{request.Scheme}://{request.Host.ToUriComponent()}");
            });
        }
    }
}
