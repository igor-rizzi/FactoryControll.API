using AutoMapper;
using FactoryControll.API.Areas.Usuarios.Models;
using FactoryControll.Domain.Entities.Usuarios;
using FactoryControll.InfraData.Models.Autenticacao;

namespace FactoryControll.API.Areas.Usuarios.Mappers
{
    public class UsuarioMappers : Profile
    {
        public UsuarioMappers()
        {
            CreateMap<UsuarioModel, Usuario>()
                .ReverseMap();

        }
    }
}
