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
    [Route("v1/usuarios")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListaUsuarios([FromServices]DataContext database){
            return await database.Usuarios.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Objeto do usuário a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> CadastraUsuario([FromServices]DataContext database, Usuario usuario){
            if(ModelState.IsValid){
                int CodUsuario = 1;
                try{
                    CodUsuario = await database.Usuarios.MaxAsync(u => u.CodUsuario) + 1;
                }catch(Exception){
                    CodUsuario = 1;
                }
                HashUtils hash = new HashUtils();
                await hash.HasheiaSenhaAsync(usuario);
                usuario.CodUsuario = CodUsuario;
                await database.Usuarios.AddAsync(usuario);
                await database.SaveChangesAsync();
                return usuario;
            }else{
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Altera um usuário
        /// </summary>
        /// <param name="CodUsuario">Codigo do usuário sendo alterado</param>
        /// <param name="usuario">Objeto do usuário com os dados alterados</param>
        /// <returns></returns>
        [HttpPut("{CodUsuario:int}")]
        public async Task<ActionResult<Usuario>> AlteraUsuario([FromServices]DataContext database, int CodUsuario, Usuario usuario){
            if(ModelState.IsValid){
                if(usuario.CodUsuario != CodUsuario){
                    return NotFound();
                }
                await database.Usuarios.AddAsync(usuario);
                await database.SaveChangesAsync();
                return usuario;
            }else{
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Desativa o acesso a um usuário
        /// </summary>
        /// <param name="CodUsuario">Código do usuário sendo desativado</param>
        /// <param name="usuario">Objeto do usuário sendo desativado</param>
        /// <returns></returns>
        [HttpDelete("{CodUsuario:int}")]
        public async Task<ActionResult<Usuario>> DesativaUsuario([FromServices]DataContext database, int CodUsuario,  Usuario usuario){
            if(ModelState.IsValid){
                if(usuario.CodUsuario != CodUsuario){
                    return BadRequest();
                }
                usuario.Ativo = "N";
                database.Entry(usuario).State = EntityState.Modified;
                try{
                    await database.SaveChangesAsync();
                }catch(Exception e){
                    return BadRequest(new {erro = e.Message});
                }
                return Ok();
            }else{
                return BadRequest();
            }
        }
    }
}