using FactoryControll.InfraData.Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FactoryControll.InfraData
{
    public class FactoryControllDbContextFactory : IDesignTimeDbContextFactory<FactoryControllDbContext>
    {
        public FactoryControllDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FactoryControllDbContext>();
            var connectionString = configuration.GetConnectionString("FactoryControllDbContext");

            optionsBuilder.UseNpgsql(connectionString);

            return new FactoryControllDbContext(optionsBuilder.Options);
        }
    }
}
