using FactoryControll.Domain.Entities.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace FactoryControll.InfraData.Models.Autenticacao
{
    public class User : IdentityUser
    {
        public long? UsuarioAppId { get; set; }

        public bool Ativo { get; set; }

        public Usuario UsuarioApp { get; set; }
    }
}
