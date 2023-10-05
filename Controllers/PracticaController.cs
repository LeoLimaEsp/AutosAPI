using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica_API.Datos;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Modelos.Dto;
using ProyectoPractica_API.Repositorio.IRepositorio;
using System.Net;

namespace ProyectoPractica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticaController : ControllerBase
    {
        //Inyeccion de LOGGIN para manadr información a la consola.
        private readonly ILogger<PracticaController> logger;
        //Inyección de DbContext para el uso de BDD
        private readonly IProyectoRepositorio _proyectoRepo;
        //Inyección de Mapper
        private readonly IMapper mapper;

        protected APIResponse _response;

        public PracticaController(ILogger<PracticaController> logger, IProyectoRepositorio proyectoRepo, IMapper mapper)
        {
            this.logger = logger;
            _proyectoRepo = proyectoRepo;
            this.mapper = mapper;
            _response = new();   
        }

        //Hacer asincronos los metodos con ASYNC, AWAIT Y TASK

        [HttpGet] //Primer endpoint tipo "Get" que nos devuelve toda la lista de carros.
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetProyectos() //ActionResult permite devolver cualquier tipo de dato.
        {
            try
            {
                logger.LogInformation("Obtener nombre de los autos existentes");
                //return Ok(ProyectoStore.proyectoList); Linea que nos regresaba una lista fija

                //Uso de mapper
                IEnumerable<Proyecto> proyectoList = await _proyectoRepo.ObtenerTodos();
                _response.Resultado = mapper.Map<IEnumerable<ProyectoDto>>(proyectoList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex) 
            {
                _response.isExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response; 
        }

        [HttpGet("id:int", Name = "GetProyecto")] //Segundo endpoint tipo "Get" que nos devuelve toda un solo carro de la lista, indicar en el verbo que sera con id.
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Tipos de respuesta que no estan documentadas se hacen el los verbos Http: [ProducesResponse] 
        public async Task<ActionResult<APIResponse>> GetProyecto(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.LogInformation("Error al traer información con el id: " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response); //Status Code
                }
                //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id); Filtrado por Id de lista fija 
                var resultado = await _proyectoRepo.Obtener(v => v.Id == id);

                if (resultado == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isExitoso = false;
                    return NotFound(_response); //Status Code
                }

                _response.Resultado = mapper.Map<ProyectoDto>(resultado);
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
        public async Task<ActionResult<APIResponse>> NuevoProyecto([FromBody] ProyectoDtoCreate NuevProyecto) //Atributo fromBody indica que enviaremos datos.
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Validación personalizada para nombres duplicados con ModelState:
                if (await _proyectoRepo.Obtener(v => v.Nombre.ToLower() == NuevProyecto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreRepetido", "Ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (NuevProyecto == null)
                {
                    return BadRequest(NuevProyecto);
                }

                Proyecto modelo = mapper.Map<Proyecto>(NuevProyecto);

                await _proyectoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProyecto", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> Delete(int id)
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
                var resultado = await _proyectoRepo.Obtener(v => v.Id == id);

                if (resultado == null)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //ProyectoStore.proyectoList.Remove(resultado);
                 await _proyectoRepo.Remover(resultado);
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
        public async Task<IActionResult> upDate(int id, [FromBody] ProyectoDtoUpdate proyectUpdate)
        {
            try
            {
                if (proyectUpdate == null || id != proyectUpdate.Id)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id);
                //resultado.Nombre = proyectUpdate.Nombre;

                Proyecto modelo = mapper.Map<Proyecto>(proyectUpdate);

                await _proyectoRepo.Actualizar(modelo);
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

        [HttpPatch("{id:int}")] //Sexto endpoint Actualizado especifico de una propiedad sin que me manden los datos actuales.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> upDatePatch (int id, JsonPatchDocument<ProyectoDtoUpdate> patchProyect)
        {
            try
            {
                if (patchProyect == null || id == 0)
                {
                    return BadRequest();
                }

                var resultado = _proyectoRepo.Obtener(v => v.Id == id, tracked: false);

                ProyectoDtoUpdate modelo = mapper.Map<ProyectoDtoUpdate>(resultado);


                if (resultado == null) return BadRequest();

                patchProyect.ApplyTo(modelo, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Proyecto modelo2 = mapper.Map<Proyecto>(modelo);

                _proyectoRepo.Actualizar(modelo2);
                return NoContent();
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
