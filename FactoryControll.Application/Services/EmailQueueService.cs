using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Application.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FactoryControll.Application.Services
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IConfiguration _configuration;

        public EmailQueueService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarParaFilaAsync(EmailMessageDto mensagem)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQ:Host"],
                    UserName = _configuration["RabbitMQ:User"],
                    Password = _configuration["RabbitMQ:Password"]
                };

                using var connection = await factory.CreateConnectionAsync("EmailPublisher");
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "email-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mensagem));

                await channel.BasicPublishAsync(
                 exchange: "",
                 routingKey: "email-queue",
                 body: body);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"Erro ao enviar mensagem para a fila: {ex.Message}");
                throw; // Re-throw or handle as needed
            }


        }
    }
}
