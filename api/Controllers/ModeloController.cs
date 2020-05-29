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
    [Route("v1/modelos")]
    public class ModeloController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Modelo>>> ListaModelos([FromServices] DataContext database) {
            return await database.Modelos.Include(m => m.Fabricante).AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("fabricantes/{CodFabricante:int}")]
        public async Task<ActionResult<List<Modelo>>> ListaModeloPorFabricante([FromServices]DataContext database, int CodFabricante){
            return await database.Modelos
                .Include(m => m.Fabricante)
                .AsNoTracking()
                .Where(m => m.CodFabricante == CodFabricante)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Modelo>> CadastraModelo([FromServices]DataContext database, [FromBody]Modelo modelo){
            if(ModelState.IsValid){
                await database.Modelos.AddAsync(modelo);
                await database.SaveChangesAsync();
                return modelo;
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}