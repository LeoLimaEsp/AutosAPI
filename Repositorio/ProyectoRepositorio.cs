using ProyectoPractica_API.Datos;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Repositorio.IRepositorio;

namespace ProyectoPractica_API.Repositorio
{
    public class ProyectoRepositorio : Repositorio<Proyecto>, IProyectoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProyectoRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<Proyecto> Actualizar(Proyecto entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Autos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;

        }
    }
}
