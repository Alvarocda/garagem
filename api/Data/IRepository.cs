using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IRepository
    {
        // Operações padrões
        Task AddAsync<T>(T entity) where T : class;
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
        

        // Veiculos
        Task<Veiculo> GetVehicle(int veiculoId, bool incluiModeloEFabricante = false);
        Task<List<Veiculo>> GetVehicles(bool incluiModeloEFabricante = false);

        // Usuários
        Task<Usuario> GetUsuario(int usuarioId);
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> CheckIfEmailIsAlreadyRegistered(string email, int usuarioId = 0);
        Task<Usuario> GetUserByEmail(string email);

        // Modelos
        Task<Modelo> GetModelo(int modeloId, bool incluiFabricante);
        Task<List<Modelo>> GetModelos(bool incluiFabricante);
    }
}