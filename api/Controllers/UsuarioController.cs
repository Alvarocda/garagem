using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Utils;
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
        public UsuarioController(DataContext context, IRepository repository)
        {
            _repository = repository;
            _context = context;
        }
        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListaUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Objeto do usuário a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> CadastraUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HashUtils hash = new HashUtils();
                await hash.HasheiaSenhaAsync(usuario);
                usuario.CriadoEm = DateTime.Now;
                await _repository.AddAsync<Usuario>(usuario);
                try
                {
                    await _repository.SaveChangesAsync();
                    return Ok();
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
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Usuario>> AlteraUsuario(int Id, Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.Id != Id)
                {
                    return NotFound();
                }
                usuario.AtualizadoEm = DateTime.Now;
                _context.Entry(usuario).State = EntityState.Modified;
                _repository.Update(usuario);
                try
                {
                    if(await _repository.SaveChangesAsync()){
                        return Ok();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    return usuario;
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
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Usuario>> DesativaUsuario(int Id, Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.Id != Id)
                {
                    return BadRequest();
                }
                usuario.Ativo = false;
                _context.Entry(usuario).State = EntityState.Modified;
                _repository.Update(usuario);
                try
                {
                    if(await _repository.SaveChangesAsync()){
                        return Ok();
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
    }
}