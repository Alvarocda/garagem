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
    public class ModeloRepository : IRepository<Modelo>
    {
        readonly DataContext _context;
        public ModeloRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Modelo entity)
        {
            await _context.Modelos.AddAsync(entity);
        }

        public void Disable(Modelo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Modelo> Find(int id)
        {
            return await _context.Modelos.FindAsync(id);
        }

        public async Task<Modelo> FirstOrDefault(Expression<Func<Modelo, bool>> predicate)
        {
            return await _context.Modelos.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Modelo>> ListAsync()
        {
            return await _context.Modelos.ToListAsync();
        }

        public async Task<List<Modelo>> ListAsync(Expression<Func<Modelo, bool>> predicate)
        {
            return await _context.Modelos.Where(predicate).ToListAsync();
        }

        public IQueryable<Modelo> Query()
        {
            return _context.Modelos;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() == 1)
                return true;

            return false;
        }

        public void Update(Modelo entity)
        {
            throw new NotImplementedException();
        }
    }
}