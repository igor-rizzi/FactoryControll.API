using FactoryControll.Application.Models;
using FactoryControll.InfraFramework.Dependency;

namespace FactoryControll.Application.Interfaces.Services
{
    public interface IColaboradorService : IScopedDependency
    {
        Task CriarColaboradorAsync(CriarColaboradorDto dto);
    }
}
