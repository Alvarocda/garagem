using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;

        }
        public async Task AddUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task DeleteUsuario(int usuarioId)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(usuarioId);
            usuario.Ativo = false;
            usuario.DesativadoEm = DateTime.Now;
        }

        public async Task<List<Usuario>> ListaTodosUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario> SelectUsuario(int usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

        public void UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}