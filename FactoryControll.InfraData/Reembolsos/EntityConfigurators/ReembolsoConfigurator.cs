using FactoryControll.Domain.Entities.Reembolsos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Reembolsos.EntityConfigurators
{
    public class ReembolsoConfigurator : IEntityTypeConfiguration<Reembolso>
    {
        public void Configure(EntityTypeBuilder<Reembolso> builder)
        {
            builder.ToTable("Reembolsos");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.DataDespesa)
                .IsRequired();

            builder.Property(r => r.ComprovanteNomeArquivo)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(r => r.ComprovanteConteudo)
                .IsRequired();

            builder.Property(r => r.ComprovanteContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Status)
                .IsRequired()
                .HasConversion<int>(); ;

            builder.Property(r => r.DataCriacao)
                .IsRequired();

            builder.HasOne(r => r.Colaborador)
                .WithMany()
                .HasForeignKey(r => r.ColaboradorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.TipoDespesa)
                .WithMany()
                .HasForeignKey(r => r.TipoDespesaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
