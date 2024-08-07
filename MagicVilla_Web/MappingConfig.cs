﻿using AutoMapper;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web {
    public class MappingConfig : Profile{
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaDTOCreated>().ReverseMap(); 
            CreateMap<VillaDTO, VillaDTOUpdated>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberDTOCreated>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberDTOUpdated>().ReverseMap();           
        }
    }
}
