using FactoryControll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryControll.Application.Interfaces.Repositorios
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
    }
}
