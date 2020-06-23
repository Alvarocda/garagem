using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IUserRepository
    {
        Task AddUsuarioAsync(Usuario usuario);
        void UpdateUsuarioAsync(Usuario usuario);
        Task<Usuario> SelectUsuario(int usuarioId);
        Task<List<Usuario>> ListaTodosUsuarios();
        Task DeleteUsuario(int usuarioId);
        Task<Usuario> GetUserByEmail(string email);
        Task<bool> SaveChangesAsync();
    }
}