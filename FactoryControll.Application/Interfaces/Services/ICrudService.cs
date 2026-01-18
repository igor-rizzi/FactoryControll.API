using FactoryControll.Domain.Common;

namespace FactoryControll.Application.Interfaces.Services
{
    public interface ICrudService<TEntity> : IBaseCrudService<TEntity>
        where TEntity : Entity
    {
    }
}
