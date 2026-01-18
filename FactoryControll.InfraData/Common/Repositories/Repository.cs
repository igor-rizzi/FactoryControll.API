using FactoryControll.Domain.Common;
using FactoryControll.InfraData.Common.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryControll.InfraData.Common.Repositories
{
    public class Repository<TEntity> : BaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly FactoryControllDbContext Context;

        public Repository(FactoryControllDbContext context) : base(context)
        {
            Context = context;
        }
    }
}
