using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Originals;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Infrastracture.Identity;
using PersonnelManagement.Server.Filters;
using PersonnelManagement.Server.Services.PaginationServices.Departments;
using PersonnelManagement.Server.Services.PaginationServices.Employees;
using PersonnelManagement.Server.Services.PaginationServices.OrderDescriptions;
using PersonnelManagement.Server.Services.PaginationServices.Orders;
using PersonnelManagement.Server.Services.PaginationServices.Originals;
using PersonnelManagement.Server.Services.PaginationServices.Positions;
using PersonnelManagement.Server.Services.UriServices;
using PersonnelManagement.Server.Validators.DepartmentEndpointsValidators;
using PersonnelManagement.Server.Validators.EmployeeEndpointsValidators;
using PersonnelManagement.Server.Validators.IdentityEndpointsValidators;
using PersonnelManagement.Server.Validators.OrderDescEndpointsValidators;
using PersonnelManagement.Server.Validators.OrderEndpointsValidators;
using PersonnelManagement.Server.Validators.OriginalEndpointsValidators;
using PersonnelManagement.Server.Validators.PositionEndpointsValidators;
using System.Text;

namespace PersonnelManagement.Api.Installers
{
    public class MvcInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllersWithViews(config => config.Filters.Add<ValidationFilter>());
            services.AddFluentValidationAutoValidation();

            #region Add validators

            services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();
            services.AddScoped<IValidator<UpdateOrderRequest>, UpdateOrderRequestValidator>();
            services.AddScoped<IValidator<CreateOrderDescriptionRequest>, CreateOrderDescriptionRequestValidator>();
            services.AddScoped<IValidator<UpdateOrderDescriptionRequest>, UpdateOrderDescriptionRequestValidator>();
            services.AddScoped<IValidator<CreateDepartmentRequest>, CreateDepartmentRequestValidator>();
            services.AddScoped<IValidator<UpdateDepartmentRequest>, UpdateDepartmentRequestValidator>();
            services.AddScoped<IValidator<CreatePositionRequest>, CreatePositionRequestValidator>();
            services.AddScoped<IValidator<UpdatePositionRequest>, UpdatePositionRequestValidator>();
            services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
            services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
            services.AddScoped<IValidator<UpdateOriginalRequest>, UpdateOriginalRequestValidator>();
            services.AddScoped<IValidator<RefreshTokenRequest>, RefreshTokenRequestValidator>();
            services.AddScoped<IValidator<UserRegistrationRequest>, UserRegistrationRequestValidator>();
            services.AddScoped<IValidator<UserLoginRequest>, UserLoginRequestValidator>();

            #endregion

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<IUriService, UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                return new UriService($"{request.Scheme}://{request.Host.ToUriComponent()}");
            });

            var jwtSettings = new JwtSettingsOptions();
            configuration.GetSection(nameof(JwtSettingsOptions)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenParams = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuerSigningKey = true
            };

            services.AddSingleton(tokenParams);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = tokenParams;
            });

            #region Add pagination services

            services.AddScoped<IOrderPaginationService, OrderPaginationService>();
            services.AddScoped<IPositionPaginationService, PositionPaginationService>();
            services.AddScoped<IDepartmentPaginationService, DepartmentPaginationService>();
            services.AddScoped<IOrderDescriptionPaginationService, OrderDescriptionPaginationService>();
            services.AddScoped<IEmployeePaginationService, EmployeePaginationService>();
            services.AddScoped<IOriginalPaginationService, OriginalPaginationService>();

            #endregion
        }
    }
}
