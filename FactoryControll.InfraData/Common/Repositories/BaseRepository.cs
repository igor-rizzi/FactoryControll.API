using FactoryControll.Application.Interfaces.Repositorios;
using FactoryControll.Domain.Common;
using FactoryControll.InfraData.Common.Context;
using FactoryControll.InfraFramework.Dependency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FactoryControll.InfraData.Common.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IScopedDependency
        where TEntity : Entity
    {
        protected readonly FactoryControllDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(FactoryControllDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }

        public void RollbackTransaction(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity GetFirst()
        {
            return DbSet.FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync()
        {
            return await DbSet.FirstOrDefaultAsync();
        }

        public virtual TEntity Get(long id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public TEntity GetIdAsNoTracking(long id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<TEntity> GetIdAsNoTrackingAsync(long id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual void Update(TEntity model)
        {
            DbSet.Attach(model);
            var current = Context.Entry(model).CurrentValues.Clone();
            Context.Entry(model).Reload();
            Context.Entry(model).CurrentValues.SetValues(current);
            Context.Entry(model).State = EntityState.Modified;

            HandleDiscriminatorProperty(model);
        }

        /// <summary>
        /// Impede a modificação da propriedade "Discriminator" em uma entidade durante operações de atualização.
        /// A propriedade "Discriminator" é usada para diferenciar tipos derivados em cenários de herança de tabelas no Entity Framework.
        /// Este método garante que o tipo da entidade não seja alterado inadvertidamente após a sua criação inicial.
        /// </summary>
        protected void HandleDiscriminatorProperty(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.Properties.Any(p => p.Metadata.Name == "Discriminator"))
                entry.Property("Discriminator").IsModified = false;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Remove(long id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
                DbSet.Remove(entity);
        }

        /// <summary>
        /// Remove as entidades filtradas do banco de dados sem carregá-las no <see cref="DbContext"/>.
        /// </summary>
        /// <param name="predicate">Uma função para testar cada elemento numa condição.</param>
        public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            DbSet.Where(predicate).ExecuteDelete();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Remove as entidades filtradas do banco de dados sem carregá-las no <see cref="DbContext"/>.
        /// </summary>
        /// <param name="predicate">Uma função para testar cada elemento numa condição.</param>
        /// <returns>Uma <see cref="Task"/> que representa a operação de remoção.</returns>
        public async Task RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await DbSet.Where(predicate).ExecuteDeleteAsync();
        }

        /// <summary>
        ///     Insere valores dos objetos no banco de dados
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        public bool Insert(IEnumerable<TEntity> items, bool cached = true)
        {
            var list = items.ToList();
            foreach (var item in list)
            {
                Add(item);
            }
            return true;
        }

        /// <summary>
        ///     Insere valor do objeto no banco de dados
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(TEntity item, bool cached = true)
        {
            await AddAsync(item);

            return true;
        }

        /// <summary>
        ///     Insere valores dos objetos no banco de dados
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(IEnumerable<TEntity> items, bool cached = true)
        {
            var list = items.ToList();
            foreach (var item in list)
            {
                await AddAsync(item);
            }
            return true;
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();

            DbSet.UpdateRange(list);

            return true;
        }

        public Task<IDbContextTransaction> BeginTransactionSerializableAsync()
        {
            throw new NotImplementedException();
        }
    }
}
