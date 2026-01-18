using FactoryControll.Domain.Entities.Colaboradores;
using FactoryControll.InfraData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Colaboradores.EntityConfigurators
{
    public class ColaboradorConfigurator : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.ToTable("Colaboradores");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(c => c.Cpf)
                .IsUnique();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.DataNascimento)
                .IsRequired();

            builder.Property(c => c.DataAdmissao)
                .IsRequired();

            builder.Property(c => c.DataRescisao)
                .IsRequired(false);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.UsuarioId)
                .IsRequired(false);

            builder.HasIndex(c => c.UsuarioId)
                .IsUnique();

            builder.Property(c => c.CargoId)
                .IsRequired();

            builder.Property(c => c.FuncaoId)
                .IsRequired();

            builder.HasOne(c => c.Cargo)
                .WithMany()
                .HasForeignKey(c => c.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Funcao)
                .WithMany()
                .HasForeignKey(c => c.FuncaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(c => c.Ativo);
        }
    }

}
