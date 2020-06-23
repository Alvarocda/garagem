using api.DTO;
using api.Models;
using AutoMapper;

namespace api.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<UsuarioDTO, Usuario>();
        }
    }
}