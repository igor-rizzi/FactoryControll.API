using FactoryControll.Application.Models;
using FactoryControll.InfraFramework.Dependency;
using Microsoft.AspNetCore.Identity;

namespace FactoryControll.Application.Interfaces.Services
{
    public interface IPasswordResetService : IScopedDependency
    {
        Task SolicitarRecuperacaoAsync(string email);
        Task<IdentityResult> RedefinirSenhaAsync(ResetarSenhaDto dto);
    }
}
