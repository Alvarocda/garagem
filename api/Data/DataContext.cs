using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Fabricante> Fabricantes {get;set;}
        public DbSet<Modelo> Modelos {get;set;}
        public DbSet<Usuario> Usuarios {get;set;}
    }
}