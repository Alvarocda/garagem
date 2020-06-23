using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
        
        Veiculo GetVehicle(int VeiculoId);
    }
}