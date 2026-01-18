using FactoryControll.Application.Interfaces.Repositorios.Usuarios;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Domain.Entities.Usuarios;

namespace FactoryControll.Application.Services
{
    public class UsuarioService : CrudService<Usuario>, IUsuarioService  
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
    }
}
