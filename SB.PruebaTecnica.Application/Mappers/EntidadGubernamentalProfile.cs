
using AutoMapper;
using SB.NombreProyecto.Domain.DTOs;
using SB.PruebaTecnica.Domain.Entities;

namespace SB.PruebaTecnica.Application.Mappers
{
    public class EntidadGubernamentalProfile : Profile
    {
        public EntidadGubernamentalProfile()
        {
            CreateMap<EntidadGubernamental, EntidadGubernamentalDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.Ubicacion!))
            ;
        }
    }
}