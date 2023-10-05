using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProyectoPractica_API.Modelos.Dto;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Repositorio.IRepositorio;
using System.Net;

namespace ProyectoPractica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroPracticaController : ControllerBase
    {
        //Inyeccion de LOGGIN para manadr información a la consola.
        private readonly ILogger<NumeroPracticaController> logger;
        //Inyección de DbContext para el uso de BDD
        private readonly IProyectoRepositorio _proyectoRepo;
        private readonly INumeroProyectoRepositorio _numeroproyectoRepo;
        //Inyección de Mapper
        private readonly IMapper mapper;

        protected APIResponse _response;

        public NumeroPracticaController(ILogger<NumeroPracticaController> logger, IProyectoRepositorio proyectoRepo, IMapper mapper, INumeroProyectoRepositorio numeroproyectoRepo)
        {
            this.logger = logger;
            _proyectoRepo = proyectoRepo;
            this.mapper = mapper;
            _response = new();
            _numeroproyectoRepo = numeroproyectoRepo;
        }

        //Hacer asincronos los metodos con ASYNC, AWAIT Y TASK

        [HttpGet] //Primer endpoint tipo "Get" que nos devuelve toda la lista de carros.
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumerosProyectos() //ActionResult permite devolver cualquier tipo de dato.
        {
            try
            {
                logger.LogInformation("Obtener numeros del proyecto");
                //return Ok(ProyectoStore.proyectoList); Linea que nos regresaba una lista fija

                //Uso de mapper
                IEnumerable<NumeroProyecto> numeroproyectoList = await _numeroproyectoRepo.ObtenerTodos();
                _response.Resultado = mapper.Map<IEnumerable<NumeroDto>>(numeroproyectoList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetNumeroProyecto")] //Segundo endpoint tipo "Get" que nos devuelve toda un solo carro de la lista, indicar en el verbo que sera con id.
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Tipos de respuesta que no estan documentadas se hacen el los verbos Http: [ProducesResponse] 
        public async Task<ActionResult<APIResponse>> GetNumeroProyecto(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.LogInformation("Error al traer el numero información con el id: " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response); //Status Code
                }
                //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id); Filtrado por Id de lista fija 
                var numeroresultado = await _numeroproyectoRepo.Obtener(v => v.ProyectoNo == id);

                if (numeroresultado == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isExitoso = false;
                    return NotFound(_response); //Status Code
                }

                _response.Resultado = mapper.Map<NumeroDto>(numeroresultado);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost] //Tercer endpoint tipo "post" para publicar un nuevo objeto en base de datos.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> NuevoNumeroProyecto([FromBody] NumeroDtoCreate NuevProyecto) //Atributo fromBody indica que enviaremos datos.
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Validación personalizada para nombres duplicados con ModelState:
                if (await _numeroproyectoRepo.Obtener(v => v.ProyectoNo == NuevProyecto.ProyectoNo) != null)
                {
                    ModelState.AddModelError("NombreRepetido", "Ese número ya existe");
                    return BadRequest(ModelState);
                }

                if(await _proyectoRepo.Obtener(v => v.Id == NuevProyecto.ProyectoId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El Id de proyecto no existe");
                    return BadRequest(ModelState);
                }

                if (NuevProyecto == null)
                {
                    return BadRequest(NuevProyecto);
                }

                NumeroProyecto modelo = mapper.Map<NumeroProyecto>(NuevProyecto);

                modelo.FechaActualizacion = DateTime.Now;
                modelo.FechaCreacion = DateTime.Now;
                await _numeroproyectoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroProyecto", new { id = modelo.ProyectoNo }, _response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpDelete("{id:int}")] //Cuarto endpoint borrado de dato
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNumero(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id);
                var numeroresultado = await _numeroproyectoRepo.Obtener(v => v.ProyectoNo == id);

                if (numeroresultado == null)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //ProyectoStore.proyectoList.Remove(resultado);
                await _numeroproyectoRepo.Remover(numeroresultado);
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return BadRequest(_response);
        }

        [HttpPut("id:int")] //Quinto endpoint Actualizado de dato.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> upDateNumero(int id, [FromBody] NumeroDtoUpdate proyectUpdate)
        {
            try
            {
                if (proyectUpdate == null || id != proyectUpdate.ProyectoNo)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id);
                //resultado.Nombre = proyectUpdate.Nombre;

                if(await _proyectoRepo.Obtener(v => v.Id == proyectUpdate.ProyectoId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El iD de proyecto no existe.");
                    return BadRequest(ModelState);
                }

                NumeroProyecto modelo = mapper.Map<NumeroProyecto>(proyectUpdate);

                await _numeroproyectoRepo.Actualizar(modelo);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return BadRequest(_response);
        }     
    }
}
