using AutoMapper;
using FactoryControll.API.Areas.Reembolsos.Models;
using FactoryControll.Domain.Entities.Reembolsos;

namespace FactoryControll.API.Areas.Reembolsos.Mappers
{
    public class ReembolsoProfile : Profile
    {
        public ReembolsoProfile()
        {
            CreateMap<ReembolsoModel, Reembolso>()
                .ForMember(dest => dest.Colaborador, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDespesa, opt => opt.Ignore());

            CreateMap<Reembolso, ReembolsoModel>();
        }
    }
}