using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProyectoPractica_API.Modelos;

namespace ProyectoPractica_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Proyecto> Autos { get; set; }
        public DbSet<NumeroProyecto> numeroProyectos { get; set; }

        //Creación de registros en tabla:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyecto>().HasData(
                new Proyecto()
                {
                    Id = 1,
                    Marca = "Volkswagen",
                    Nombre = "Tiguan",
                    Hp = 135,
                    ImagenUrl = "",
                    Precio = 539000,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                },
                new Proyecto()
                {
                    Id = 2,
                    Marca = "Cupra",
                    Nombre = "Ateca",
                    Hp = 500,
                    ImagenUrl = "",
                    Precio = 959000,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                },
                new Proyecto()
                {
                    Id = 3,
                    Marca = "Porsche",
                    Nombre = "911 GT3-RS",
                    Hp = 525,
                    ImagenUrl = "",
                    Precio = 4778000,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                });
        }
    }
}
