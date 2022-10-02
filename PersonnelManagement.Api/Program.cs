using PersonnelManagement.Api.Installers;
using PersonnelManagement.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// TODO: add jwt/facebook/google authorization to api

builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

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

var swaggerOptions = new SwaggerOptions();
builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

app.UseSwagger(opt => opt.RouteTemplate = swaggerOptions.RouteTemplate);
app.UseSwaggerUI(opt => opt.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));

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
