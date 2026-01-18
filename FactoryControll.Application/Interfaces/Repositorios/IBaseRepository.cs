using FactoryControll.InfraFramework.Dependency;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FactoryControll.Application.Interfaces.Repositorios
{
    public interface IBaseRepository<TEntity> : IScopedDependency
        where TEntity : class
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<IDbContextTransaction> BeginTransactionSerializableAsync();

        IDbContextTransaction BeginTransaction();

        void CommitTransaction(IDbContextTransaction transaction);

        void RollbackTransaction(IDbContextTransaction transaction);

        void Add(TEntity entity);

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        TEntity GetFirst();

        Task<TEntity> GetFirstAsync();

        TEntity Get(long id);

        TEntity GetIdAsNoTracking(long id);

        Task<TEntity> GetIdAsNoTrackingAsync(long id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsNoTracking();

        void Update(TEntity entity);

        void Remove(long id);

        void Remove(TEntity entity);

        void RemoveRange(Expression<Func<TEntity, bool>> predicate);

        Task RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();

        int SaveChanges();

        bool Insert(IEnumerable<TEntity> items, bool cached = true);


        Task<bool> InsertAsync(TEntity item, bool cached = true);

        bool Update(IEnumerable<TEntity> entities);
    }
}
