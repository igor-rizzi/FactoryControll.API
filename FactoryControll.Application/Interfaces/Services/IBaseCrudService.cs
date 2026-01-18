using FactoryControll.Domain.Common;
using FactoryControll.InfraFramework.Dependency;

namespace FactoryControll.Application.Interfaces.Services
{
    public interface IBaseCrudService<TEntity> : IScopedDependency where TEntity : Entity
    {
        Task<TEntity> GetFirstAsync();

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> InsertAndSaveAsync(TEntity entity);

        Task SaveChangesAsync();

        Task<TEntity> UpdateAndSaveAsync(TEntity entity);

        Task<IEnumerable<TEntity>> UpdateAndSaveAsync(IEnumerable<TEntity> entities);

        Task<TEntity> Save(TEntity entity);

        Task DeleteAndSaveAsync(long id);

        IQueryable<TEntity> GetAll();

        IEnumerable<TEntity> Listar();

        TEntity Get(long id);

        TEntity GetAsNoTracking(long id);

        Task<TEntity> GetAsNoTrackingAsync(long id);
    }
}
