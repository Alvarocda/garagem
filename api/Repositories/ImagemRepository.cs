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
    public class ImagemRepository : IRepository<Imagem>
    {
        readonly DataContext _context;
        public ImagemRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Imagem entity)
        {
            await _context.Imagens.AddAsync(entity);
        }

        public async Task<Imagem> Find(int id)
        {
            return await _context.Imagens.FindAsync(id);
        }

        public async Task<Imagem> FirstOrDefault(Expression<Func<Imagem, bool>> predicate)
        {
            return await _context.Imagens.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Imagem>> ListAsync()
        {
            return await _context.Imagens.ToListAsync();
        }

        public async Task<List<Imagem>> ListAsync(Expression<Func<Imagem, bool>> predicate)
        {
            return await _context.Imagens.Where(predicate).ToListAsync();
        }

        public IQueryable<Imagem> Query()
        {
            return _context.Imagens;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() == 1)
                return true;
                
            return false;
        }

        public Task UpdateAsync(Imagem entity)
        {
            throw new NotImplementedException();
        }
    }
}