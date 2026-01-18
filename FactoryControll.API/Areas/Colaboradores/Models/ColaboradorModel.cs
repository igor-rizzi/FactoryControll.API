using FactoryControll.Application.Interfaces;

namespace FactoryControll.API.Areas.Colaboradores.Models
{
    public class ColaboradorModel : IModel
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public long CargoId { get; set; }
        public long FuncaoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
    }
}
