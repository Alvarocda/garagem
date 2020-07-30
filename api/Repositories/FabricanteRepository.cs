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
    public class FabricanteRepository : IRepository<Fabricante>
    {
        readonly DataContext _context;
        public FabricanteRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Fabricante entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<Fabricante> Find(int id)
        {
            return await _context.Fabricantes.FindAsync(id);
        }

        public async Task<Fabricante> FirstOrDefault(Expression<Func<Fabricante, bool>> predicate)
        {
            return await _context.Fabricantes.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Fabricante>> ListAsync()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        public async Task<List<Fabricante>> ListAsync(Expression<Func<Fabricante, bool>> predicate)
        {
            return await _context.Fabricantes.Where(predicate).ToListAsync();
        }

        public IQueryable<Fabricante> Query()
        {
            return _context.Fabricantes;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() == 1)
                return true;
                
            return false;
        }

        public Task UpdateAsync(Fabricante entity)
        {
            throw new NotImplementedException();
        }
    }
}