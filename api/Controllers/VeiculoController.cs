using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class VeiculoController : ControllerBase
    {
        private readonly IRepository<Veiculo> _repository;
        public VeiculoController(IRepository<Veiculo> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "administrador,usuario")]
        public async Task<ActionResult<List<Veiculo>>> GetVeiculos([FromQuery] bool listaFabricantes)
        {
            IQueryable<Veiculo> veiculos = _repository.Query();
            if (listaFabricantes)
            {
                veiculos = veiculos.Include(v => v.Fabricante);
            }
            return await veiculos.OrderByDescending(v => v.Id).ToListAsync();
        }

        [Authorize(Roles = "administrador, usuario")]
        [HttpPost]
        public async Task<ActionResult<Veiculo>> CadastraVeiculo([FromBody] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.CriadoPor = User.RetornaIdUsuario();
                await _repository.AddAsync(veiculo);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(new { status = true, veiculo });
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [Authorize(Roles = "administrador,usuario")]
        [HttpPut("{veiculoId}")]
        public async Task<ActionResult<Veiculo>> UpdateVeiculo([FromRoute] int veiculoId, [FromBody] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.AtualizadoPor = User.RetornaIdUsuario();
                _repository.Update(veiculo);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(new { status = true, veiculo });
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("{veiculoId}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<object>> DeleteVeiculo([FromRoute] int veiculoId)
        {
            Veiculo veiculo = await _repository.Find(veiculoId);
            if (veiculo == null)
            {
                return BadRequest();
            }
            veiculo.DesativadoPor = User.RetornaIdUsuario();
            _repository.Disable(veiculo);
            if (await _repository.SaveChangesAsync())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}