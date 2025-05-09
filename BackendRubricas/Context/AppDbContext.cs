using BackendRubricas.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendRubricas.Context
{
    public class AppDbContext : DbContext
    {



        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TipoDocumento> tipoDocumento { get; set; }

    }

}
