using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FactoryControll.InfraData.Seeds
{
    public static class IdentitySeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                "Administrador",
                "AnalistaFinanceiro",
                "Colaborador"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedRoleClaimsAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleClaims = new Dictionary<string, List<Claim>>
            {
                {
                    "Administrador", new List<Claim>
                    {
                        new Claim("Permissao", "AcessoTotal")
                    }
                },
                {
                    "AnalistaFinanceiro", new List<Claim>
                    {
                        new Claim("Permissao", "Reembolso:Listar"),
                        new Claim("Permissao", "Reembolso:Visualizar"),
                        new Claim("Permissao", "Reembolso:Aprovar"),
                        new Claim("Permissao", "Relatorio:Visualizar") 
                    }
                },
                {
                    "Colaborador", new List<Claim>
                    {
                        new Claim("Permissao", "Reembolso:Listar"),
                        new Claim("Permissao", "Reembolso:Criar"),
                        new Claim("Permissao", "Reembolso:Editar"),
                        new Claim("Permissao", "Reembolso:Cancelar")
                    }
                }
            };

            foreach (var roleName in roleClaims.Keys)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null) continue;

                var existingClaims = await roleManager.GetClaimsAsync(role);

                foreach (var claim in roleClaims[roleName])
                {
                    if (!existingClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                    {
                        await roleManager.AddClaimAsync(role, claim);
                    }
                }
            }
        }
        public static async Task SeedAdminUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@desafio.com";
            string adminUserName = "admin";
            string adminPassword = "Senha@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new User
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                };

                var createUserResult = await userManager.CreateAsync(user, adminPassword);

                if (createUserResult.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("Administrador"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Administrador"));
                    }

                    await userManager.AddToRoleAsync(user, "Administrador");
                }
                else
                {
                    throw new Exception($"Erro ao criar usuário admin: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
