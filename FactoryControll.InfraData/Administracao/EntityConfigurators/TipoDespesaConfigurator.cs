using FactoryControll.Domain.Entities.Administracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryControll.InfraData.Administracao.EntityConfigurators
{
    public class TipoDespesaConfigurator : IEntityTypeConfiguration<TipoDespesa>
    {
        public void Configure(EntityTypeBuilder<TipoDespesa> builder)
        {
            builder.ToTable("TiposDespesa");

            builder.HasKey(td => td.Id);

            builder.Property(td => td.Descricao)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
