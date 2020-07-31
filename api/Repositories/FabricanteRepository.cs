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

        public void Disable(Fabricante entity)
        {
            entity.DesativadoEm = DateTime.Now;
            entity.Ativo = false;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(f => f.CriadoEm).IsModified = false;
            _context.Entry(entity).Property(f => f.CriadoPor).IsModified = false;
            _context.Entry(entity).Property(f => f.AtualizadoEm).IsModified = false;
            _context.Entry(entity).Property(f => f.AtualizadoPor).IsModified = false;
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
            if (await _context.SaveChangesAsync() == 1)
                return true;

            return false;
        }

        public void Update(Fabricante entity)
        {
            entity.AtualizadoEm = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(f => f.CriadoEm).IsModified = false;
            _context.Entry(entity).Property(f => f.CriadoPor).IsModified = false;
        }
    }
}