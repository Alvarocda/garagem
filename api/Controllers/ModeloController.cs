using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class ModeloController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly DataContext _context;
        public ModeloController(DataContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;

        }
        [HttpGet]
        public async Task<ActionResult<List<Modelo>>> ListaModelos()
        {
            return await _context.Modelos.Include(m => m.Fabricante).AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("fabricante/{fabricanteId:int}")]
        public async Task<ActionResult<List<Modelo>>> ListaModeloPorFabricante(int fabricanteId)
        {
            return await _context.Modelos
                .AsNoTracking()
                .Where(m => m.FabricanteId == fabricanteId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Modelo>> CadastraModelo([FromBody] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.CriadoEm = DateTime.Now;
                await _repository.AddAsync(modelo);
                if(await _repository.SaveChangesAsync()){
                    return modelo;
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