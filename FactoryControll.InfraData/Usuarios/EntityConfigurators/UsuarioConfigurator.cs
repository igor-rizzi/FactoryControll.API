using FactoryControll.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Usuarios.EntityConfigurators
{
    public class UsuarioConfigurator : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.TipoUsuario)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.ColaboradorId)
                .IsRequired(false);

            builder.Property(u => u.UserLoginId)
                .IsRequired(false);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne(u => u.Colaborador)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Usuario>(u => u.ColaboradorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}