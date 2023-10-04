using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica_API.Modelos.Dto
{
    public class ProyectoDtoCreate
    {
        public string Nombre { get; set; }
        public int Hp { get; set; }
        public int Precio { get; set; }
        public string Marca { get; set; }
        public string ImagenUrl { get; set; }
    }
}
