using System.ComponentModel.DataAnnotations;

namespace FactoryControll.Application.Models
{
    public class ResetarSenhaDto
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Token é obrigatório")]
        public string Token { get; set; } = default!;

        [Required(ErrorMessage = "Nova senha é obrigatória")]
        public string NovaSenha { get; set; } = default!;

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarNovaSenha { get; set; } = default!;
    }
}
