using System.ComponentModel;

namespace FactoryControll.Domain.Entities.Reembolsos.Enums
{
    public enum Status
    {
        [Description("Pendente de Aprovação Financeira")]
        PendenteAprovacaoFinanceira = 1,

        [Description("Aprovado")]
        Aprovado = 2,

        [Description("Reprovado")]
        Reprovado = 3,

        [Description("Pago")]
        Pago = 4
    }
}
