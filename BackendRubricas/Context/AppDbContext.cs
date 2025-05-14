using BackendReciclarsipaga.Models;
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
        public DbSet<Ciudad> ciudad { get; set; }
        public DbSet<Barrio> barrio { get; set; }
        public DbSet<Persona> persona { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Recoleccion> recoleccion { get; set; }
        public DbSet<RecoleccionDto> recoleccionDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecoleccionDto>().HasNoKey();
        }

    }

}
