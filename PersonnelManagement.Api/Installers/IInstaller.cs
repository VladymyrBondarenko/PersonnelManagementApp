namespace PersonnelManagement.Api.Installers
{
    internal interface IInstaller
    {
        void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}