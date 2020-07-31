using api.Models;

namespace api.Interfaces
{
    public interface IUsuarioRepository<T> : IRepository<Usuario> where T : Usuario
    {
        void UpdatePassword(T entity);
    }
}