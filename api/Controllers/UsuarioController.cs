using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Models;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public UsuarioController(DataContext context, IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _context = context;
        }
        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "administrador,usuario")]
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListaUsuarios()
        {
            return await _repository.GetUsuarios();
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Objeto do usuário a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> CadastraUsuario(UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario jaExiste = await _repository.CheckIfEmailIsAlreadyRegistered(usuario.Email);
                if(jaExiste != null){
                    return BadRequest(new {status = false, message = $"Já existe um usuário com o email {usuario.Email} cadastrado, por favor, utilize outro"});
                }
                HashUtils hash = new HashUtils();
                Usuario novoUsuario = _mapper.Map<Usuario>(usuario);
                await hash.HasheiaSenhaAsync(novoUsuario, usuario.SenhaString);
                novoUsuario.CriadoEm = DateTime.Now;
                novoUsuario.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync<Usuario>(novoUsuario);
                try
                {
                    if(await _repository.SaveChangesAsync()){
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
        /// <param name="Id">Codigo do usuário sendo alterado</param>
        /// <param name="usuario">Objeto do usuário com os dados alterados</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> AlteraUsuario(int id, Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.Id != id)
                {
                    return NotFound();
                }
                Usuario jaExiste = await _repository.CheckIfEmailIsAlreadyRegistered(usuario.Email, id);
                if(jaExiste != null){
                    return BadRequest(new {status = false, message = $"Já existe um usuário com o email {usuario.Email} cadastrado, por favor, utilize outro"});
                }
                usuario.AtualizadoEm = DateTime.Now;
                usuario.AtualizadoPor = User.RetornaIdUsuario();
                _context.Entry(usuario).State = EntityState.Modified;
                _context.Entry(usuario).Property(u => u.Senha).IsModified = false;
                _context.Entry(usuario).Property(u => u.Chave).IsModified = false;
                _context.Entry(usuario).Property(u => u.CriadoEm).IsModified = false;
                _context.Entry(usuario).Property(u => u.CriadoPor).IsModified = false;
                _context.Entry(usuario).Property(u => u.DesativadoEm).IsModified = false;
                _context.Entry(usuario).Property(u => u.DesativadoPor).IsModified = false;
                try
                {
                    if(await _repository.SaveChangesAsync()){
                        return Ok(new {status = true, message = "Usuário alterado com sucesso!"});
                    }
                    return BadRequest();
                }
                catch (Exception e)
                {
                    return BadRequest(new { status = false, message = e.Message, innerErro = e.InnerException != null ? e.InnerException.Message : "" });
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
        /// <param name="Id">Código do usuário sendo desativado</param>
        /// <param name="usuario">Objeto do usuário sendo desativado</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Usuario>> DesativaUsuario(int id)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _repository.GetUsuario(id);
                if(usuario == null){
                    return NotFound();
                }
                usuario.Ativo = false;
                usuario.DesativadoEm = DateTime.Now;
                usuario.DesativadoPor = User.RetornaIdUsuario();
                _context.Entry(usuario).State = EntityState.Modified;
                try
                {
                    if(await _repository.SaveChangesAsync()){
                        return Ok(new {status = true, message = "Usuário desativado com sucesso!"});
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

        [HttpPatch]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<dynamic>> AlteraSenhaUsuario(UsuarioDTO usuarioDto){
            Usuario usuario = await _repository.GetUsuario(usuarioDto.Id);
            if(usuario == null){
                return BadRequest();
            }
            HashUtils hash = new HashUtils();
            await hash.HasheiaSenhaAsync(usuario, usuarioDto.SenhaString);
            _context.Entry(usuario).State = EntityState.Modified;
            try{
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, message = "Senha alterada com sucesso!"});
                }
                return BadRequest();
            }catch(Exception e){
                return BadRequest();
            }
        }

    }
}