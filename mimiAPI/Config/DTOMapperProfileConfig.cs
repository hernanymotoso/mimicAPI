using AutoMapper;
using mimiAPI.Models;
using mimiAPI.Models.DTOs;

namespace mimiAPI.Config
{
    public class DTOMapperProfileConfig : Profile 
    {
        public DTOMapperProfileConfig()
        {
            CreateMap<Palavra, PalavraDTO>();
        }
    }
}