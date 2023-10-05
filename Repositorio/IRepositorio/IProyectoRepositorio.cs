using ProyectoPractica_API.Modelos;

namespace ProyectoPractica_API.Repositorio.IRepositorio
{
    public interface IProyectoRepositorio : IRepositorio<Proyecto>
    {
        Task<Proyecto> Actualizar(Proyecto entidad);
    }
}
