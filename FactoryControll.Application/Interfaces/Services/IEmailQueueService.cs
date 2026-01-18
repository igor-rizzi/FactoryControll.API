using FactoryControll.Application.Models;
using FactoryControll.InfraFramework.Dependency;

namespace FactoryControll.Application.Interfaces.Services
{
    public interface IEmailQueueService : IScopedDependency
    {

        Task EnviarParaFilaAsync(EmailMessageDto mensagem);
    }
}
