using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Interfaces;
using api.Models;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository<Usuario> _repository;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository<Usuario> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "administrador,usuario")]
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> ListaUsuarios()
        {
            List<UsuarioDTO> usuarioDTOs = _mapper.Map<List<UsuarioDTO>>(await _repository.ListAsync());
            return usuarioDTOs;
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Objeto do usuário a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> CadastraUsuario([FromBody] UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario jaExiste = await _repository.FirstOrDefault(u => u.Email.ToLower() == usuario.Email.ToLower());
                if (jaExiste != null)
                {
                    return BadRequest(new { message = $"Já existe um usuário com o email {usuario.Email} cadastrado, por favor, utilize outro" });
                }
                HashUtils hash = new HashUtils();
                Usuario novoUsuario = _mapper.Map<Usuario>(usuario);
                await hash.HasheiaSenhaAsync(novoUsuario, usuario.SenhaString);
                novoUsuario.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync(novoUsuario);
                try
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Altera um usuário
        /// </summary>
        /// <param name="id">Codigo do usuário sendo alterado</param>
        /// <param name="usuarioDto">Objeto do usuário com os dados alterados</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> AlteraUsuario([FromRoute] int id, [FromBody] UsuarioDTO usuarioDto)
        {
            if (ModelState.IsValid)
            {
                if (usuarioDto.Id != id)
                {
                    return NotFound();
                }
                Usuario jaExiste = await _repository.FirstOrDefault(u => u.Id != usuarioDto.Id && u.Email.ToLower() == usuarioDto.Email.ToLower());
                if (jaExiste != null)
                {
                    return BadRequest(new { message = $"Já existe um usuário com o email {usuarioDto.Email} cadastrado, por favor, utilize outro" });
                }

                Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

                usuario.AtualizadoPor = User.RetornaIdUsuario();
                _repository.Update(usuario);
                try
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(new { message = "Usuário alterado com sucesso!" });
                    }
                    return BadRequest();
                }
                catch (Exception e)
                {
                    return BadRequest(new { message = e.Message, innerErro = e.InnerException != null ? e.InnerException.Message : "" });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Desativa o acesso a um usuário
        /// </summary>
        /// <param name="id">Código do usuário sendo desativado</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> DesativaUsuario([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _repository.Find(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                usuario.DesativadoPor = User.RetornaIdUsuario();
                _repository.Disable(usuario);
                try
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(new { message = "Usuário desativado com sucesso!" });
                    }
                    return BadRequest();
                }
                catch (Exception e)
                {
                    return BadRequest(new { erro = e.Message });
                }
            }
            return BadRequest();
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<dynamic>> AlteraSenhaUsuario([FromRoute] int id, [FromBody] UsuarioDTO usuarioDto)
        {
            if (usuarioDto.Id != id)
            {
                return NotFound();
            }
            Usuario usuario = await _repository.Find(usuarioDto.Id);
            if (usuario == null)
            {
                return BadRequest();
            }
            HashUtils hash = new HashUtils();
            await hash.HasheiaSenhaAsync(usuario, usuarioDto.SenhaString);
            _repository.UpdatePassword(usuario);
            try
            {
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(new { message = "Senha alterada com sucesso!" });
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="key">Objeto do usuário a ser cadastrado</param>
        /// <returns></returns>
        [HttpGet("firstuser")]
        //[Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> CreateFirstUser([FromHeader] string key)
        {
            if (key == "184420")
            {
                Usuario jaExiste = await _repository.FirstOrDefault(u => u.Email.ToLower() == "alvaro.claro@hotmail.com");
                if (jaExiste != null)
                {
                    return BadRequest(new { status = false, message = $"Usuário admininistrador padrão já cadastrado!" });
                }
                HashUtils hash = new HashUtils();
                Usuario usuario = new Usuario
                {
                    Nome = "Administrador",
                    Email = "alvaro.claro@hotmail.com",
                    Role = "administrador",
                    CriadoPor = 0
                };
                await hash.HasheiaSenhaAsync(usuario, "123");
                await _repository.AddAsync(usuario);
                try
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                catch (Exception e)
                {
                    return BadRequest(new { msg = e.Message, innerMessage = e.InnerException != null ? e.InnerException.Message : "" });
                }
            }
            else
            {
                return BadRequest();
            }

        }
    }

}
