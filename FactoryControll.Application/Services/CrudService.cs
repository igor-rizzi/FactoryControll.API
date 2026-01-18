using FactoryControll.Application.Interfaces.Repositorios;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Domain.Common;

namespace FactoryControll.Application.Services
{
    public class CrudService<TEntity> : BaseCrudService<TEntity>, ICrudService<TEntity> where TEntity : Entity
    {
        public CrudService(IBaseRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
