using AutoMapper;
using FactoryControll.API.Areas.Administracao.Models;
using FactoryControll.Domain.Entities.Administracao;

namespace FactoryControll.API.Areas.Administracao.Mappers
{
    public class AdministracaoMappers : Profile
    {
        public AdministracaoMappers()
        {
            CargoMappers();
            FuncaoMappers();
            TipoDespesaMappers();
        }

        private void TipoDespesaMappers()
        {
            CreateMap<TipoDespesaModel, TipoDespesa>()
                .ReverseMap();
        }

        private void CargoMappers()
        {
            CreateMap<CargoModel, Cargo>()
                .ReverseMap();
        }

        private void FuncaoMappers()
        {
            CreateMap<FuncaoModel, Funcao>()
               .ReverseMap();
        }
    }
}
