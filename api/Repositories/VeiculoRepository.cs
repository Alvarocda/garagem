using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class VeiculoRepository : IRepository<Veiculo>
    {
        readonly DataContext _context;
        public VeiculoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Veiculo entity)
        {
            await _context.Veiculos.AddAsync(entity);
        }

        public void Disable(Veiculo entity)
        {
            entity.Ativo = false;
            entity.Status = "R";
            entity.DesativadoEm = DateTime.Now;
            _context.Entry(entity).Property(v => v.AtualizadoEm).IsModified = false;
            _context.Entry(entity).Property(v => v.AtualizadoPor).IsModified = false;
            _context.Entry(entity).Property(v => v.CriadoEm).IsModified = false;
            _context.Entry(entity).Property(v => v.CriadoPor).IsModified = false;
        }

        public async Task<Veiculo> Find(int id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task<Veiculo> FirstOrDefault(Expression<Func<Veiculo, bool>> predicate)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Veiculo>> ListAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<List<Veiculo>> ListAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            return await _context.Veiculos.Where(predicate).ToListAsync();
        }

        public IQueryable<Veiculo> Query()
        {
            return _context.Veiculos;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() == 1)
                return true;

            return false;
        }

        public void Update(Veiculo entity)
        {
            entity.AtualizadoEm = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(v => v.CriadoEm).IsModified = false;
            _context.Entry(entity).Property(v => v.CriadoPor).IsModified = false;
            _context.Entry(entity).Property(v => v.AtualizadoEm).IsModified = false;
            _context.Entry(entity).Property(v => v.AtualizadoPor).IsModified = false;
        }
    }
}