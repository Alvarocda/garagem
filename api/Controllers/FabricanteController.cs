using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
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
        [HttpGet]
        public async Task<ActionResult<List<Fabricante>>> ListaFabricantes()
        {
            return await _context.Fabricantes.AsNoTracking().ToListAsync();
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