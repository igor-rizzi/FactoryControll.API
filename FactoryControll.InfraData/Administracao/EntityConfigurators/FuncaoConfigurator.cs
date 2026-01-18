using FactoryControll.Domain.Entities.Administracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Administracao.EntityConfigurators
{
    public class FuncaoConfigurator : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.ToTable("Funcoes");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(f => f.Nome)
                .IsUnique();

            builder.HasMany(f => f.Colaboradores)
                .WithOne(c => c.Funcao)
                .HasForeignKey(c => c.FuncaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
