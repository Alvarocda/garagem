using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class ModeloController : ControllerBase
    {
        private readonly IRepository<Modelo> _repository;
        public ModeloController(IRepository<Modelo> repository)
        {
            _repository = repository;

        }
        [HttpGet]
        public async Task<ActionResult<List<Modelo>>> ListaModelos([FromQuery] bool incluiFabricante = false, [FromQuery] bool incluiDesativados = false)
        {
            IQueryable<Modelo> modelos = _repository.Query();
            if (incluiFabricante)
            {
                modelos = modelos.Include(m => m.Fabricante);
            }
            if (incluiDesativados)
            {
                modelos = modelos.Where(m => m.Ativo == false);
                return await modelos
                    .OrderByDescending(m => m.Id)
                    .ToListAsync();
            }
            return await modelos
                .OrderByDescending(m => m.Id)
                .Where(m => m.Ativo == true)
                .ToListAsync();

        }

        [HttpPost]
        [Authorize("administrador,usuario")]
        public async Task<ActionResult<Modelo>> CadastraModelo([FromBody] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                Modelo jaExiste = await _repository.FirstOrDefault(m => m.FabricanteId == modelo.FabricanteId && m.Nome.ToLower() == modelo.Nome.ToLower());
                if (jaExiste != null)
                {
                    return BadRequest(new { status = false, message = $"O modelo {modelo.Nome} já esta cadastrado" });
                }
                modelo.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync(modelo);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(new { status = true, message = "Modelo cadastrado com sucesso!", modelo });
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}