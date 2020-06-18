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
        public async Task<ActionResult<List<Veiculo>>> GetVeiculos(){
            return await _context.Veiculos.AsNoTracking().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Veiculo>> CadastraVeiculo(Veiculo veiculo){
            if(ModelState.IsValid){
                veiculo.CriadoEm = DateTime.Now;
                await _repository.AddAsync(veiculo);
                if(await _repository.SaveChangesAsync()){
                    return veiculo;
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPut("{veiculoId}")]
        public async Task<ActionResult<Veiculo>> UpdateVeiculo(int veiculoId, Veiculo veiculo){
            if(ModelState.IsValid){
                _context.Entry(veiculo).State = EntityState.Modified;
                veiculo.AtualizadoEm = DateTime.Now;
                await _repository.AddAsync(veiculo);
                if(await _repository.SaveChangesAsync()){
                    return Ok(veiculo);
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("{veiculoId}")]
        public async Task<ActionResult<object>> DeleteVeiculo(int veiculoId){
            Veiculo veiculo = await _context.Veiculos.FindAsync(veiculoId);
            if(veiculo == null){
                return BadRequest();
            }
            veiculo.Ativo = false;
            veiculo.Status = "R";
            veiculo.DesativadoEm = DateTime.Now;
            _context.Entry(veiculo).State = EntityState.Modified;
            _repository.Update(veiculo);
            if(await _repository.SaveChangesAsync()){
                return Ok();
            }
            return BadRequest();
        }
    }
}