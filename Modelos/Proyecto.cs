using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPractica_API.Modelos
{
    public class Proyecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Data annotation para aumentar el Id automaticamente.
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Hp { get; set; }
        public int Precio { get; set; }
        public string Marca { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
