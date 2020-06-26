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
        Task<List<Veiculo>> GetVehicles(bool incluiModeloEFabricante = false, bool incluiDesativados = false);

        // Usuários
        Task<Usuario> GetUsuario(int usuarioId);
        Task<List<Usuario>> GetUsuarios(bool incluiDesativados = false);
        Task<Usuario> CheckIfEmailIsAlreadyRegistered(string email, int usuarioId);
        Task<Usuario> GetUserByEmail(string email);

        // Modelos
        Task<Modelo> GetModelo(int modeloId, bool incluiFabricante);
        Task<List<Modelo>> GetModelos(bool incluiFabricante, bool incluiDesativados = false);
        Task<Modelo> CheckIfModeloIsAlreadyRegistered(int fabricanteId, string nome);

        // Fabricante

        Task<Fabricante> GetFabricante(int fabricanteId, bool incluiModelos = false);
        Task<List<Fabricante>> GetFabricantes(bool incluiModelos = false, bool incluiDesativados = false);
        Task<Fabricante> GetFabricanteByName(string nome);
        Task<Fabricante> CheckIfFabricanteIsAlreadyRegisted(string nome, int id = 0);
    }
}