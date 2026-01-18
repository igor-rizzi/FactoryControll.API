using FactoryControll.Application.Interfaces;
using FactoryControll.Domain.Entities.Reembolsos.Enums;

namespace FactoryControll.API.Areas.Reembolsos.Models
{
    public class ReembolsoModel : IModel
    {
        public long Id { get; set; }

        public required string Titulo { get; set; } = string.Empty;

        public required string Descricao { get; set; } = string.Empty;

        public required decimal Valor { get; set; }

        public required DateTime DataDespesa { get; set; }

        public required string ComprovanteNomeArquivo { get; set; } = string.Empty;

        public required byte[] ComprovanteConteudo { get; set; } = Array.Empty<byte>();

        public required string ComprovanteContentType { get; set; } = string.Empty;

        public Status Status { get; set; } = Status.PendenteAprovacaoFinanceira;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public required long ColaboradorId { get; set; }

        public required long TipoDespesaId { get; set; }


    }
}
