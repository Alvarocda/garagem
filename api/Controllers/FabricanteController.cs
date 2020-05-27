using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/fabricantes")]
    public class FabricanteController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Fabricante>>> ListaFabricantes([FromServices] DataContext database){
            return await database.Fabricantes.AsNoTracking().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Fabricante>> CadastraFabricante([FromServices] DataContext database, [FromBody]Fabricante fabricante){
            if(ModelState.IsValid){
                await database.Fabricantes.AddAsync(fabricante);
                await database.SaveChangesAsync();
                return fabricante;
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}