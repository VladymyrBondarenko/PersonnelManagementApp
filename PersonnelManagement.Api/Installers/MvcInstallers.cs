namespace PersonnelManagement.Api.Installers
{
    public class MvcInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();
        }
    }
}
