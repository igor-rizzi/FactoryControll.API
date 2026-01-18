using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FactoryControll.InfraData.Common.Context
{
    public class FactoryControllDbContext : IdentityDbContext<User>
    {
        public FactoryControllDbContext(DbContextOptions<FactoryControllDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
