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
        public async Task<ActionResult<List<Modelo>>> ListaModelos([FromQuery] bool incluiFabricante = false, [FromQuery] bool incluiDesativados = false)
        {
            return await _repository.GetModelos(incluiFabricante, incluiDesativados);
        }

        [HttpPost]
        public async Task<ActionResult<Modelo>> CadastraModelo([FromBody] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                Modelo jaExiste = await _repository.CheckIfModeloIsAlreadyRegistered(modelo.FabricanteId, modelo.Nome);
                if(jaExiste != null){
                    return BadRequest(new {status = false, message = $"O modelo {modelo.Nome} já esta cadastrado"});
                }
                modelo.CriadoEm = DateTime.Now;
                modelo.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync(modelo);
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, message = "Modelo cadastrado com sucesso!", modelo});
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