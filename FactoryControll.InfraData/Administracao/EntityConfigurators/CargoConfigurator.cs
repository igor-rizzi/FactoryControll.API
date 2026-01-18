using FactoryControll.Domain.Entities.Administracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Administracao.EntityConfigurators
{
    public class CargoConfigurator : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("Cargos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Nome)
                .IsUnique();

            builder.HasMany(c => c.Colaboradores)
                .WithOne(c => c.Cargo)
                .HasForeignKey(c => c.CargoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
