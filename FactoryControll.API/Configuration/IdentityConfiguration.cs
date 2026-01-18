using FactoryControll.InfraData.Common.Context;
using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FactoryControll.API.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FactoryControllDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("FactoryControllDbContext")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FactoryControllDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AcessoTotal", policy =>
                    policy.RequireClaim("Permissao", "AcessoTotal"));

                options.AddPolicy("ReembolsoCriar", policy =>
                    policy.RequireClaim("Permissao", "Reembolso:Criar"));

                options.AddPolicy("ReembolsoEditar", policy =>
                    policy.RequireClaim("Permissao", "Reembolso:Editar"));

                options.AddPolicy("ReembolsoCancelar", policy =>
                    policy.RequireClaim("Permissao", "Reembolso:Cancelar"));

                options.AddPolicy("ReembolsoVisualizar", policy =>
                    policy.RequireClaim("Permissao", "Reembolso:Visualizar"));

                options.AddPolicy("ReembolsoAprovar", policy =>
                    policy.RequireClaim("Permissao", "Reembolso:Aprovar"));

                options.AddPolicy("RelatorioVisualizar", policy =>
                    policy.RequireClaim("Permissao", "Relatorio:Visualizar"));
            });

            return services;
        }
    }
}
