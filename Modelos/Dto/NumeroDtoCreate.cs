using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica_API.Modelos.Dto
{
    public class NumeroDtoCreate
    {
        [Required]
        public int ProyectoNo { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
