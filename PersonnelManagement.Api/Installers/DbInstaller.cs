using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Identities;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Domain.Models.Identity;
using PersonnelManagement.Infrastracture.DbContexts;
using PersonnelManagement.Infrastracture.Departments;
using PersonnelManagement.Infrastracture.Employees;
using PersonnelManagement.Infrastracture.FileOperations;
using PersonnelManagement.Infrastracture.FileOperations.Originals;
using PersonnelManagement.Infrastracture.Identity;
using PersonnelManagement.Infrastracture.Orders.OrderBase;
using PersonnelManagement.Infrastracture.Positions;

namespace PersonnelManagement.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("PersonnelManagementDataConnection")));

            services.AddDefaultIdentity<IdentityUserModel>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddScoped<IIdentityService, IdentityService>();
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
