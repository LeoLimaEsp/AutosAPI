using AutoMapper;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Modelos.Dto;

namespace ProyectoPractica_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Proyecto, ProyectoDto>();
            CreateMap<ProyectoDto, Proyecto>();

            CreateMap<Proyecto, ProyectoDtoCreate>().ReverseMap();
            CreateMap<Proyecto, ProyectoDtoUpdate>().ReverseMap();

            CreateMap<NumeroProyecto, NumeroDto>().ReverseMap();
            CreateMap<NumeroProyecto, NumeroDtoCreate>().ReverseMap();
            CreateMap<NumeroProyecto, NumeroDtoUpdate>().ReverseMap();
        }

    }
}
