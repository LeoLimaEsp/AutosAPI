using System.Net;

namespace ProyectoPractica_API.Modelos
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get ; set; }
        public bool isExitoso { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Resultado { get; set; }

    }
}
