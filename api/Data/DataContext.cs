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
        public DbSet<Veiculo> Veiculos {get;set;}
        public DbSet<Imagem> Imagens {get;set;}
        public DbSet<TipoVeiculo> TiposVeiculo {get;set;}
    }
}