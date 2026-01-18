using FactoryControll.Application.Interfaces.Repositorios.Usuarios;
using FactoryControll.Domain.Entities.Usuarios;
using FactoryControll.InfraData.Common.Context;
using FactoryControll.InfraData.Common.Repositories;

namespace FactoryControll.InfraData.Usuarios.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FactoryControllDbContext context) : base(context)
        {
        }
    }
}
