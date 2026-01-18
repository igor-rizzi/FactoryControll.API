using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryControll.Application.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IEmailQueueService _emailQueueService;

        public ColaboradorService(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public async Task CriarColaboradorAsync(CriarColaboradorDto dto)
        {
            // 1. Criar colaborador no banco
            // 2. Criar usuário com senha temporária
            var senhaTemporaria = GerarSenha();
            var userName = GerarUserName(dto.Nome); // Ex: joao_silva

            // 3. Criar e-mail
            var mensagem = new EmailMessageDto
            {
                Para = dto.Email,
                Assunto = "Bem-vindo ao Sistema de Reembolsos",
                CorpoHtml = @$"
                <p>Olá, {dto.Nome}!</p>
                <p>Seu acesso ao sistema foi criado.</p>
                <p><strong>Usuário:</strong> {userName}</p>
                <p><strong>Senha:</strong> {senhaTemporaria}</p>
                <p>Acesse: <a href=""https://sistema.exemplo.com"">sistema.exemplo.com</a></p>
                <p>Recomendamos que troque sua senha no primeiro acesso.</p>
                <p>Atenciosamente,<br/>Equipe de Suporte</p>"
            };

            await _emailQueueService.EnviarParaFilaAsync(mensagem);
        }

        private string GerarSenha()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789@#$";
            return new string(Enumerable.Range(0, 10)
                .Select(_ => chars[new Random().Next(chars.Length)]).ToArray());
        }

        private string GerarUserName(string nomeCompleto)
        {
            var partes = nomeCompleto.Split(' ');
            return partes.Length >= 2
                ? $"{partes[0].ToLower()}_{partes[^1].ToLower()}"
                : partes[0].ToLower();
        }
    }
}
