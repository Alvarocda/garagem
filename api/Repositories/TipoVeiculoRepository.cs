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
    public class TipoVeiculoRepository : IRepository<TipoVeiculo>
    {
        readonly DataContext _context;
        public TipoVeiculoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TipoVeiculo entity)
        {
            await _context.TiposVeiculo.AddAsync(entity);
        }

        public void Disable(TipoVeiculo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TipoVeiculo> Find(int id)
        {
            return await _context.TiposVeiculo.FindAsync(id);
        }

        public async Task<TipoVeiculo> FirstOrDefault(Expression<Func<TipoVeiculo, bool>> predicate)
        {
            return await _context.TiposVeiculo.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TipoVeiculo>> ListAsync()
        {
            return await _context.TiposVeiculo.ToListAsync();
        }

        public async Task<List<TipoVeiculo>> ListAsync(Expression<Func<TipoVeiculo, bool>> predicate)
        {
            return await _context.TiposVeiculo.Where(predicate).ToListAsync();
        }

        public IQueryable<TipoVeiculo> Query()
        {
            return _context.TiposVeiculo;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() == 1)
                return true;

            return false;
        }

        public void Update(TipoVeiculo entity)
        {
            throw new System.NotImplementedException();
        }
    }
}