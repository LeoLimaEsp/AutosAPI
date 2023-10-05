using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPractica_API.Modelos
{
    public class NumeroProyecto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] //Propieadad indicando que nosotros indicaremos el número de Id MANUALMENTE        
        public int ProyectoNo { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto proyecto { get; set; }

        public string DetalleEspecial { get ; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
