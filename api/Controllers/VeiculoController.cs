using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly DataContext _context;
        public VeiculoController(DataContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles= "administrador,usuario")]
        public async Task<ActionResult<List<Veiculo>>> GetVeiculos([FromQuery] bool listaFabricantes){
            return await _repository.GetVehicles(listaFabricantes);
        }

        [Authorize(Roles = "administrador, usuario")]
        [HttpPost]
        public async Task<ActionResult<Veiculo>> CadastraVeiculo([FromBody]Veiculo veiculo){
            if(ModelState.IsValid){
                veiculo.CriadoEm = DateTime.Now;
                veiculo.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync(veiculo);
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, veiculo});
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [Authorize(Roles = "administrador,usuario")]
        [HttpPut("{veiculoId}")]
        public async Task<ActionResult<Veiculo>> UpdateVeiculo([FromRoute] int veiculoId, [FromBody]Veiculo veiculo){
            if(ModelState.IsValid){
                _context.Entry(veiculo).State = EntityState.Modified;
                veiculo.AtualizadoEm = DateTime.Now;
                veiculo.AtualizadoPor = User.RetornaIdUsuario();
                _context.Entry(veiculo).State = EntityState.Modified;
                _context.Entry(veiculo).Property(v => v.CriadoEm).IsModified = false;
                _context.Entry(veiculo).Property(v => v.CriadoPor).IsModified = false;
                if(await _repository.SaveChangesAsync()){
                    return Ok(new {status = true, veiculo});
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("{veiculoId}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<object>> DeleteVeiculo([FromRoute] int veiculoId){
            Veiculo veiculo = await _repository.GetVehicle(veiculoId);
            if(veiculo == null){
                return BadRequest();
            }
            veiculo.Ativo = false;
            veiculo.Status = "R";
            veiculo.DesativadoEm = DateTime.Now;
            veiculo.DesativadoPor = User.RetornaIdUsuario();
            _context.Entry(veiculo).State = EntityState.Modified;
            _context.Entry(veiculo).Property(v => v.CriadoEm).IsModified = false;
            _context.Entry(veiculo).Property(v => v.CriadoPor).IsModified = false;
            _context.Entry(veiculo).Property(v => v.AtualizadoEm).IsModified = false;
            _context.Entry(veiculo).Property(v => v.AtualizadoPor).IsModified = false;
            if(await _repository.SaveChangesAsync()){
                return Ok();
            }
            return BadRequest();
        }
    }
}