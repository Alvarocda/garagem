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
    public class UsuarioRepository : IRepository<Usuario>
    {
        readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Usuario entity)
        {
            await _context.Usuarios.AddAsync(entity);
        }

        public async Task<Usuario> Find(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> FirstOrDefault(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Usuario>> ListAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<List<Usuario>> ListAsync(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.Where(predicate).ToListAsync();
        }

        public IQueryable<Usuario> Query()
        {
            return _context.Usuarios;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() == 1)
                return true;
                
            return false;
        }

        public Task UpdateAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}