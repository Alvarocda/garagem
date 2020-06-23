using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Models;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public AuthController(DataContext context, IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _context = context;

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login(UsuarioDTO authUsuario)
        {
            Usuario usuario = await _repository.GetUserByEmail(authUsuario.Email);
            if (usuario == null)
            {
                return NotFound(new { status = false, message = "Usuário não encontrado" });
            }
            if (!usuario.Ativo)
            {
                return BadRequest(new { status = false, message = "Usuário desativado" });
            }
            HashUtils hashUtils = new HashUtils();
            bool senhaValida = await hashUtils.VerificaSenhaHashAsync(usuario, authUsuario.SenhaString);
            if (!senhaValida)
            {
                return BadRequest(new { status = false, message = "Usuário ou senha incorretos" });
            }
            string token = TokenService.GenerateToken(usuario);
            return Ok(new { status = true, token = token });
        }
    }
}