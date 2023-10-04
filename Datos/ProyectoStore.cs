using ProyectoPractica_API.Modelos.Dto;

namespace ProyectoPractica_API.Datos
{
    public static class ProyectoStore
    {
        public static List<ProyectoDto> proyectoList = new List<ProyectoDto>
        {
            new ProyectoDto{Id=1, Nombre="Porsche 911"},
            new ProyectoDto{Id=2, Nombre="Porsche carrera Gt-3"}
        };
    }
}
