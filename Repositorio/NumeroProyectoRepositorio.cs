using ProyectoPractica_API.Datos;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Repositorio.IRepositorio;

namespace ProyectoPractica_API.Repositorio
{
    public class NumeroProyectoRepositorio : Repositorio<NumeroProyecto>, INumeroProyectoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroProyectoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroProyecto> Actualizar(NumeroProyecto entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.numeroProyectos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;

        }
    }
}
