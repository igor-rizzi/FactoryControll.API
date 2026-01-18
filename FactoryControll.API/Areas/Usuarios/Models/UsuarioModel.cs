using FactoryControll.Application.Interfaces;
using FactoryControll.Domain.Entities.Usuarios.Enums;

namespace FactoryControll.API.Areas.Usuarios.Models
{
    public class UsuarioModel : IModel
    {
        public long Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }

        public TipoUsuario TipoUsuario { get; set; } = TipoUsuario.Colaborador;
    }
}
