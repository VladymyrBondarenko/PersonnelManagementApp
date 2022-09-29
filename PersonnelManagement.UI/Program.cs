using PersonnelManagement.Application.DbContexts;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Infrastructure;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Infrastracture.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Infrastracture.Orders.OrderBase;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Infrastracture.Positions;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Infrastracture.Departments;
using PersonnelManagement.Infrastracture.FileOperations;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PersonnelManagementDataConnection");
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderFactory, OrderFactory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDescriptionService, OrderDescriptionService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFtpService, FtpService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
