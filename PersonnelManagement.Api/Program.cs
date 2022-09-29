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
using PersonnelManagement.Infrastracture.Departments;
using PersonnelManagement.Infrastracture.Employees;
using PersonnelManagement.Infrastracture.FileOperations;
using PersonnelManagement.Infrastracture.FileOperations.Originals;
using PersonnelManagement.Infrastracture.Orders.OrderBase;
using PersonnelManagement.Infrastracture.Positions;
using PersonnelManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. TODO: move installers to separate files

// Mvc installers
var connectionString = builder.Configuration.GetConnectionString("IdentityManagementConnection");
builder.Services.AddDbContext<IdentitiesDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentitiesDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

// Db installers
var connString = builder.Configuration.GetConnectionString("PersonnelManagementDataConnection");
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt.UseSqlServer(connString));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderFactory, OrderFactory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDescriptionService, OrderDescriptionService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

//Ftp installers
var ftpSettings = new FtpClientSettings();
builder.Configuration.GetSection(nameof(FtpClientSettings)).Bind(ftpSettings);
builder.Services.AddSingleton(ftpSettings);

var ftpStructSettings = new FtpStructureSettings();
builder.Configuration.GetSection(nameof(FtpStructureSettings)).Bind(ftpStructSettings);
builder.Services.AddSingleton(ftpStructSettings);

builder.Services.AddScoped<IFtpService, FtpService>();
builder.Services.AddScoped<IOriginalService, OriginalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
