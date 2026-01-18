using System.ComponentModel;

namespace FactoryControll.Domain.Entities.Usuarios.Enums
{
    public enum TipoUsuario
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Colaborador")]
        Colaborador = 2,

        [Description("Analista Financeiro")]
        AnalistaFinanceiro = 3
    }
}
