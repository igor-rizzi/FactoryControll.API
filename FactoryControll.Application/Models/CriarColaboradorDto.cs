using System.ComponentModel.DataAnnotations;

namespace FactoryControll.Application.Models
{
    public class CriarColaboradorDto
    {
        [Required]
        public string Cpf { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = default!;

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }

        public DateTime? DataRescisao { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Cargo { get; set; } = default!;

        [Required]
        public string Funcao { get; set; } = default!;
    }
}
