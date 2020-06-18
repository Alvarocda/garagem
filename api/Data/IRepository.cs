using System.Threading.Tasks;

namespace api.Data
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        void AddA<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
        
    }
}