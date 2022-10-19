using PersonnelManagement.Server.Services.PaginationServices.Departments;
using PersonnelManagement.Server.Services.PaginationServices.Employees;
using PersonnelManagement.Server.Services.PaginationServices.OrderDescriptions;
using PersonnelManagement.Server.Services.PaginationServices.Orders;
using PersonnelManagement.Server.Services.PaginationServices.Positions;
using PersonnelManagement.Server.Services.UriServices;

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

            services.AddScoped<IOrderPaginationService, OrderPaginationService>();
            services.AddScoped<IPositionPaginationService, PositionPaginationService>();
            services.AddScoped<IDepartmentPaginationService, DepartmentPaginationService>();
            services.AddScoped<IOrderDescriptionPaginationService, OrderDescriptionPaginationService>();
            services.AddScoped<IEmployeePaginationService, EmployeePaginationService>();
        }
    }
}
