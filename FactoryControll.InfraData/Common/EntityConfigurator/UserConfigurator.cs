using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryControll.InfraData.Common.EntityConfigurator
{
    public class UserConfigurator : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");

            builder
            .HasOne(a => a.UsuarioApp)
            .WithOne()
            .HasForeignKey<User>(a => a.UsuarioAppId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
