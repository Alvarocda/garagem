using System.Threading.Tasks;
using api.Models;

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

        public Veiculo GetVehicle(int VeiculoId)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges<T>(T entity) where T : class
        {
            return (_context.SaveChanges() > 0);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update<T>(entity);
        }
    }
}