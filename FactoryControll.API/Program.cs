using FactoryControll.API.Configuration;
using FactoryControll.InfraData.Common.Context;
using FactoryControll.InfraData.Models.Autenticacao;
using FactoryControll.InfraData.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbConfiguration(builder.Configuration);

builder.Services.ConfigureDependencyInjection();

builder.Services.AddAuthenticationConfig(builder.Configuration);

builder.Services.AddAuthorizationWithPolicies();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var db = scope.ServiceProvider.GetRequiredService<FactoryControllDbContext>();

    db.Database.Migrate();

    await IdentitySeedData.SeedRolesAsync(roleManager);
    await IdentitySeedData.SeedRoleClaimsAsync(roleManager);
    await IdentitySeedData.SeedAdminUserAsync(userManager, roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseExceptionMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
