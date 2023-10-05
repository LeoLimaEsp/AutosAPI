using ProyectoPractica_API.Modelos;
namespace ProyectoPractica_API.Repositorio.IRepositorio
{
    public interface INumeroProyectoRepositorio : IRepositorio<NumeroProyecto>
    {
        Task<NumeroProyecto> Actualizar(NumeroProyecto entidad);
    }
}
