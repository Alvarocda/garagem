using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Repository : IRepository
    {
        #region Controles genericos do repositorio
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

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
        #endregion

        #region Modelo
        public async Task<Modelo> GetModelo(int modeloId, bool incluiFabricante = false)
        {
            IQueryable<Modelo> query = _context.Modelos.AsNoTracking();
            if(incluiFabricante){
                query = query.Include(m => m.Fabricante);
            }

            query = query.OrderBy(m => m.Id)
                        .Where(m => m.Id == modeloId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Modelo>> GetModelos(bool incluiFabricante = false, bool incluiDesativados = false)
        {
            IQueryable<Modelo> query = _context.Modelos.AsNoTracking();
            if(incluiFabricante){
                query = query.Include(m => m.Fabricante);
            }
            if(incluiDesativados){
                return await query.OrderBy(m => m.Id).Where(m => m.Ativo == true || m.Ativo == false).ToListAsync();
            }
            return await query.Where(m => m.Ativo == true).OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Modelo> CheckIfModeloIsAlreadyRegistered(int fabricanteId, string nome){
            return await _context.Modelos
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.FabricanteId == fabricanteId && m.Nome.ToLower() == nome.ToLower());
        }

        #endregion
        
        #region Usuario
        public async Task<Usuario> GetUsuario(int usuarioId)
        {
            return await _context.Usuarios
                        .FindAsync(usuarioId);
        }

        public async Task<List<Usuario>> GetUsuarios(bool incluiDesativados = false)
        {
            if(incluiDesativados){
                return await _context.Usuarios
                        .AsNoTracking()
                        .Where(u => u.Ativo == false || u.Ativo == true)
                        .ToListAsync();
            }
            return await _context.Usuarios
                        .AsNoTracking()
                        .Where(u => u.Ativo == true)
                        .ToListAsync();
        }
        public async Task<Usuario> CheckIfEmailIsAlreadyRegistered(string email, int usuarioId)
        {
            return await _context.Usuarios
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Id != usuarioId);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        #endregion

        #region Veiculo
        public async Task<Veiculo> GetVehicle(int veiculoId, bool incluiModeloEFabricante = false)
        {
            IQueryable<Veiculo> query = _context.Veiculos.AsNoTracking();

            if(incluiModeloEFabricante){
                query = query.Include(v => v.Modelo)
                            .ThenInclude(m => m.Fabricante);
            }
            query = query.OrderBy(v => v.Id).Where(v => v.Id == veiculoId);
           
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Veiculo>> GetVehicles(bool incluiModeloEFabricante = false, bool incluiDesativados = false)
        {
            IQueryable<Veiculo> query = _context.Veiculos.AsNoTracking();

            if(incluiModeloEFabricante){
                query = query.Include(v => v.Modelo)
                            .ThenInclude(m => m.Fabricante);
            }
            if(incluiDesativados){
                return await query.Where(v => v.Ativo == false || v.Ativo == true).OrderBy(v => v.Id).ToListAsync();
            }
            return await query.Where(v => v.Ativo == true).OrderBy(v => v.Id).ToListAsync();
        }

        #endregion
        
        #region Fabricante
        public async Task<Fabricante> GetFabricante(int fabricanteId, bool incluiModelos = false)
        {
            IQueryable<Fabricante> query = _context.Fabricantes.AsNoTracking();
            if(incluiModelos){
                query = query.Include(f => f.Modelos);
            }

            return await query.FirstOrDefaultAsync(f => f.Id == fabricanteId);
        }

        public async Task<List<Fabricante>> GetFabricantes(bool incluiModelos = false, bool incluiDesativados = false)
        {
            IQueryable<Fabricante> query = _context.Fabricantes.AsNoTracking();

            if(incluiModelos){
                query = query.Include(f => f.Modelos);
            }
            if(incluiDesativados){
                return await query.Where(f => f.Ativo == false || f.Ativo == true).OrderBy(f => f.Id).ToListAsync();
            }
            return await query.Where(f => f.Ativo == true).OrderBy(f => f.Id) .ToListAsync();
        }

        public async Task<Fabricante> GetFabricanteByName(string nome)
        {
            return await _context.Fabricantes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Nome.ToLower() == nome.ToLower());
        }

        public async Task<Fabricante> CheckIfFabricanteIsAlreadyRegisted(string nome, int id)
        {
            return await _context.Fabricantes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Nome.ToLower() == nome.ToLower() && f.Id != id);
        }
        #endregion
    }
}