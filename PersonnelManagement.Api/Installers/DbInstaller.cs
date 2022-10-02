using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Api.Data;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Infrastracture.DbContexts;
using PersonnelManagement.Infrastracture.Departments;
using PersonnelManagement.Infrastracture.Employees;
using PersonnelManagement.Infrastracture.FileOperations;
using PersonnelManagement.Infrastracture.FileOperations.Originals;
using PersonnelManagement.Infrastracture.Orders.OrderBase;
using PersonnelManagement.Infrastracture.Positions;

namespace PersonnelManagement.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<IdentitiesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityManagementConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentitiesDbContext>();

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("PersonnelManagementDataConnection")));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDescriptionService, OrderDescriptionService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            // set up ftp services
            var ftpSettings = new FtpClientSettings();
            configuration.GetSection(nameof(FtpClientSettings)).Bind(ftpSettings);
            services.AddSingleton(ftpSettings);

            var ftpStructSettings = new FtpStructureSettings();
            configuration.GetSection(nameof(FtpStructureSettings)).Bind(ftpStructSettings);
            services.AddSingleton(ftpStructSettings);

            services.AddScoped<IFtpService, FtpService>();
            services.AddScoped<IOriginalService, OriginalService>();
            services.AddScoped<IOriginalRepository, OriginalRepository>();
        }
    }
}
