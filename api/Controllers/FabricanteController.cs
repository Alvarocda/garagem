using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        private readonly IRepository _repository;
        private readonly DataContext _context;
        public FabricanteController(DataContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;

        }
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public async Task<ActionResult<List<Fabricante>>> ListaFabricantes()
        {
            return await _context.Fabricantes.AsNoTracking().ToListAsync();
        }

        [HttpGet("modelos/{fabricanteId}")]
        public async Task<ActionResult<Fabricante>> ListaModelosDoFabricante(int fabricanteId){
            return await _context.Fabricantes.AsNoTracking().Include(f => f.Modelos).Where(f => f.Id == fabricanteId).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Fabricante>> CadastraFabricante( [FromBody] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                fabricante.CriadoEm = DateTime.Now;
                await _repository.AddAsync(fabricante);
                if(await _repository.SaveChangesAsync()){
                    return fabricante;
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<ActionResult<Fabricante>> UpdateFabricante(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                fabricante.AtualizadoEm = DateTime.Now;
                _context.Entry(fabricante).State = EntityState.Modified;
                if(await _repository.SaveChangesAsync()){
                    return fabricante;
                }
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Fabricante>> DeleteFabricante(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                fabricante.DesativadoEm = DateTime.Now;
                if(await _repository.SaveChangesAsync()){
                    return fabricante;
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