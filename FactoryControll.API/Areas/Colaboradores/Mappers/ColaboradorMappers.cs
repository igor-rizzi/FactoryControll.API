using AutoMapper;
using FactoryControll.API.Areas.Colaboradores.Models;
using FactoryControll.Domain.Entities.Colaboradores;

namespace FactoryControll.API.Areas.Colaboradores.Mappers
{
    public class ColaboradorMappers : Profile
    {
        public ColaboradorMappers()
        {
            CreateMap<Colaborador, ColaboradorModel>()
                .ReverseMap();
        }
    }
}
