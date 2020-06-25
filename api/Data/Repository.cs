using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Repository : IRepository
    {
        readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add<T>(entity);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync<T>(entity);
        }

        

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove<T>(entity);
        }

        public async Task<Modelo> GetModelo(int modeloId, bool incluiFabricante = false)
        {
            IQueryable<Modelo> query = _context.Modelos;
            if(incluiFabricante){
                query = query.Include(m => m.Fabricante);
            }

            query = query.AsNoTracking()
                        .OrderBy(m => m.Id)
                        .Where(m => m.Id == modeloId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Modelo>> GetModelos(bool incluiFabricante)
        {
            IQueryable<Modelo> query = _context.Modelos;
            if(incluiFabricante){
                query = query.Include(m => m.Fabricante);
            }

            return await query.AsNoTracking()
                        .OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Usuario> GetUsuario(int usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }
        public async Task<Usuario> CheckIfEmailIsAlreadyRegistered(string email, int usuarioId = 0)
        {
            if(usuarioId == 0){
                return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            }
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email && u.Id != usuarioId);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Veiculo> GetVehicle(int veiculoId, bool incluiModeloEFabricante = false)
        {
            IQueryable<Veiculo> query = _context.Veiculos;

            if(incluiModeloEFabricante){
                query = query.AsNoTracking()
                            .Include(v => v.Modelo)
                            .ThenInclude(m => m.Fabricante);
            }
            query = query.AsNoTracking().OrderBy(v => v.Id).Where(v => v.Id == veiculoId);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Veiculo>> GetVehicles(bool incluiModeloEFabricante = false)
        {
            IQueryable<Veiculo> query = _context.Veiculos;

            if(incluiModeloEFabricante){
                query = query.AsNoTracking()
                            .Include(v => v.Modelo)
                            .ThenInclude(m => m.Fabricante);
            }
            return await query.AsNoTracking().OrderBy(v => v.Id).ToListAsync();
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        
    }
}