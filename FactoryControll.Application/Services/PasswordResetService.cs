using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FactoryControll.Application.Services
{
    public class PasswordResetService<TUser> : IPasswordResetService where TUser : IdentityUser
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IEmailQueueService _emailQueueService;
        private readonly IConfiguration _configuration;

        public PasswordResetService(UserManager<TUser> userManager, IEmailQueueService emailQueueService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailQueueService = emailQueueService;
            _configuration = configuration;
        }

        public async Task SolicitarRecuperacaoAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return; // Silent fail — don't reveal if email exists

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"{_configuration["FrontEnd:ResetPasswordUrl"]}?email={email}&token={encodedToken}";

            var mensagem = new EmailMessageDto
            {
                Para = email,
                Assunto = "Recuperação de Senha - FactoryControll",
                CorpoHtml = GerarCorpoEmail(resetLink)
            };

            await _emailQueueService.EnviarParaFilaAsync(mensagem);
        }

        public async Task<IdentityResult> RedefinirSenhaAsync(ResetarSenhaDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

            var decodedToken = Uri.UnescapeDataString(dto.Token);
            return await _userManager.ResetPasswordAsync(user, decodedToken, dto.NovaSenha);
        }

        private static string GerarCorpoEmail(string resetLink)
        {
            return $@"
<div style=""font-family:Arial,sans-serif;max-width:600px;margin:0 auto"">
  <div style=""background-color:#1a56db;padding:24px;text-align:center"">
    <h1 style=""color:#ffffff;margin:0"">FactoryControll</h1>
  </div>
  <div style=""padding:32px;background-color:#f9fafb"">
    <h2 style=""color:#111827"">Recuperação de Senha</h2>
    <p style=""color:#374151"">Recebemos uma solicitação para redefinir a senha da sua conta.</p>
    <p style=""color:#374151"">Clique no botão abaixo para criar uma nova senha:</p>
    <div style=""text-align:center;margin:32px 0"">
      <a href=""{resetLink}"" style=""background-color:#1a56db;color:#ffffff;padding:14px 28px;text-decoration:none;border-radius:6px;font-weight:bold;display:inline-block"">
        Redefinir Senha
      </a>
    </div>
    <p style=""color:#6b7280;font-size:14px"">Este link expira em 1 hora.</p>
    <p style=""color:#6b7280;font-size:14px"">Se você não solicitou a recuperação de senha, ignore este email.</p>
  </div>
  <div style=""background-color:#e5e7eb;padding:16px;text-align:center"">
    <p style=""color:#6b7280;font-size:12px;margin:0"">© FactoryControll. Todos os direitos reservados.</p>
  </div>
</div>";
        }
    }
}
