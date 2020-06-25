using api.DTO;
using api.Models;
using AutoMapper;

namespace api.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}