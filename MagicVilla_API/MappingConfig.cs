using AutoMapper;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;

namespace MagicVilla_API {
    public class MappingConfig : Profile{
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();  
            CreateMap<Villa, VillaDTOCreated>().ReverseMap();  
            CreateMap<Villa, VillaDTOUpdated>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOCreated>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOUpdated>().ReverseMap();

            CreateMap<LocalUser, RegistrationRequestDTO>().ReverseMap();
            //CreateMap<LocalUser, LoginResponseDTO>().ReverseMap();
        }
    }
}
