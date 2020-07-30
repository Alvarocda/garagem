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
    [Route("v1/[controller]")]
    public class FabricanteController : ControllerBase
    {
        private readonly IRepository<Fabricante> _repository;
        private readonly DataContext _context;
        public FabricanteController(DataContext context, IRepository<Fabricante> repository)
        {
            _context = context;
            _repository = repository;

        }
        [HttpGet]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<List<Fabricante>>> ListaFabricantes([FromQuery] bool listamodelos = false, [FromQuery] bool listaDesativados = false)
        {
            IQueryable<Fabricante> fabricantes = _repository.Query();
            if(listamodelos){
                fabricantes = fabricantes.Include(f => f.Modelos);
            }
            if(listaDesativados){
                return await fabricantes
                .OrderByDescending(f => f.Id)
                .ToListAsync();
            }
            return await fabricantes
                .Where(f => f.Ativo == true)
                .OrderByDescending(f => f.Id)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<Fabricante>> SelecionaFabricante([FromRoute] int id, [FromQuery] bool listaModelos = false){
            if(listaModelos){
                IQueryable<Fabricante> fabricante = _repository.Query();
                fabricante = fabricante.Include(f => f.Modelos);
                return await fabricante.FirstOrDefaultAsync(f => f.Id == id);
            }
            return await _repository.Find(id);
        }

        [HttpPost]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<Fabricante>> CadastraFabricante([FromBody] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                Fabricante jaExiste = await _repository.FirstOrDefault(f => f.Nome.ToLower() == fabricante.Nome.ToLower());
                if(jaExiste != null){
                    return BadRequest(new {status = false, message = $"Já existe um fabricante com o nome {fabricante.Nome} cadastrado!"});
                }
                await _repository.AddAsync(fabricante);
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, message = "Fabricante cadastrado com sucesso!", fabricante});
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Fabricante>> UpdateFabricante([FromRoute] int id, [FromBody] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                fabricante.AtualizadoEm = DateTime.Now;
                fabricante.AtualizadoPor = User.RetornaIdUsuario();
                _context.Entry(fabricante).State = EntityState.Modified;
                _context.Entry(fabricante).Property(f => f.CriadoEm).IsModified = false;
                _context.Entry(fabricante).Property(f => f.CriadoPor).IsModified = false;
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, message = "Fabricante alterado com sucesso!", fabricante});
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<Fabricante>> DesativaFabricante([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                Fabricante fabricante = await _repository.Find(id);
                if(fabricante == null){
                    return NotFound();
                }
                fabricante.DesativadoEm = DateTime.Now;
                fabricante.DesativadoPor = User.RetornaIdUsuario();
                fabricante.Ativo = false;
                _context.Entry(fabricante).State = EntityState.Modified;
                _context.Entry(fabricante).Property(f => f.CriadoEm).IsModified = false;
                _context.Entry(fabricante).Property(f => f.CriadoPor).IsModified = false;
                _context.Entry(fabricante).Property(f => f.AtualizadoEm).IsModified = false;
                _context.Entry(fabricante).Property(f => f.AtualizadoPor).IsModified = false;
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, message = "Fabricante desativado com sucesso!"});
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