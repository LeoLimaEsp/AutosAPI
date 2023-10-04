using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica_API.Datos;
using ProyectoPractica_API.Modelos;
using ProyectoPractica_API.Modelos.Dto;

namespace ProyectoPractica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticaController : ControllerBase
    {
        //Inyeccion de LOGGIN para manadr información a la consola.
        private readonly ILogger<PracticaController> logger;
        //Inyección de DbContext para el uso de BDD
        private readonly ApplicationDbContext db;
        //Inyección de Mapper
        private readonly IMapper mapper;

        public PracticaController(ILogger<PracticaController> logger, ApplicationDbContext db, IMapper mapper)
        {

            this.logger = logger;
            this.db = db;
            this.mapper = mapper;

        }

        //Hacer asincronos los metodos con ASYNC, AWAIT Y TASK

        [HttpGet] //Primer endpoint tipo "Get" que nos devuelve toda la lista de carros.
        public async Task<ActionResult<IEnumerable<ProyectoDto>>> GetProyectos() //ActionResult permite devolver cualquier tipo de dato.
        {
            logger.LogInformation("Obtener nombre de los autos existentes");
            //return Ok(ProyectoStore.proyectoList); Linea que nos regresaba una lista fija

            //Uso de mapper
            IEnumerable<Proyecto> proyectoList = await db.Autos.ToListAsync();
            return Ok(mapper.Map<IEnumerable<ProyectoDto>>(proyectoList));
        }

        [HttpGet("id:int", Name = "GetProyecto")] //Segundo endpoint tipo "Get" que nos devuelve toda un solo carro de la lista, indicar en el verbo que sera con id.
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Tipos de respuesta que no estan documentadas se hacen el los verbos Http: [ProducesResponse] 
        public async Task<ActionResult<ProyectoDto>> GetProyecto(int id)
        {
            if (id == 0)
            {
                logger.LogInformation("Error al traer información con el id: " + id);
                return BadRequest(); //Status Code
            }
            //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id); Filtrado por Id de lista fija 
            var resultado = await db.Autos.FirstOrDefaultAsync(v => v.Id == id);

            if (resultado == null)
            {
                return NotFound(); //Status Code
            }

            return Ok(mapper.Map<ProyectoDto>(resultado));
        }

        [HttpPost] //Tercer endpoint tipo "post" para publicar un nuevo objeto en base de datos.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProyectoDto>> NuevoProyecto([FromBody] ProyectoDtoCreate NuevProyecto) //Atributo fromBody indica que enviaremos datos.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validación personalizada para nombres duplicados con ModelState:
            if (await db.Autos.FirstOrDefaultAsync(v => v.Nombre.ToLower() == NuevProyecto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreRepetido", "Ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (NuevProyecto == null)
            {
                return BadRequest(NuevProyecto);
            }

            //NuevProyecto.Id = ProyectoStore.proyectoList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1; para anexar a lista fija
            //ProyectoStore.proyectoList.Add(NuevProyecto);

            Proyecto modelo = mapper.Map<Proyecto>(NuevProyecto);

            //El mappeo de arriba sustituye las lineas de abajo:
            /*
            Proyecto modelo = new()
            {         
                Nombre = NuevProyecto.Nombre,
                Marca = NuevProyecto.Marca,
                Hp = NuevProyecto.Hp,
                Precio = NuevProyecto.Precio,
                ImagenUrl = NuevProyecto.ImagenUrl
            };
            */

            await db.Autos.AddAsync(modelo);
            await db.SaveChangesAsync();

            //return Ok(NuevProyecto);
            return CreatedAtRoute("GetProyecto", new { id = modelo.Id }, modelo);
        }

        [HttpDelete("{id:int}")] //Cuarto endpoint borrado de dato
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id);
            var resultado = await db.Autos.FirstOrDefaultAsync(v => v.Id == id);

            if (resultado == null)
            {
                return NotFound();
            }

            //ProyectoStore.proyectoList.Remove(resultado);
            db.Autos.Remove(resultado);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("id:int")] //Quinto endpoint Actualizado de dato.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> upDate(int id, [FromBody] ProyectoDtoUpdate proyectUpdate)
        {
            if (proyectUpdate == null || id != proyectUpdate.Id)
            {
                return BadRequest();
            }

            //var resultado = ProyectoStore.proyectoList.FirstOrDefault(v => v.Id == id);
            //resultado.Nombre = proyectUpdate.Nombre;

            Proyecto modelo = mapper.Map<Proyecto>(proyectUpdate); 
            
            db.Autos.Update(modelo);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")] //Sexto endpoint Actualizado especifico de una propiedad sin que me manden los datos actuales.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> upDatePatch (int id, JsonPatchDocument<ProyectoDtoUpdate> patchProyect)
        {
            if (patchProyect == null || id == 0)
            {
                return BadRequest();
            }

            var resultado = await db.Autos.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            ProyectoDtoUpdate modelo = mapper.Map<ProyectoDtoUpdate>(resultado);
        

            if (resultado == null) return BadRequest();
             
            patchProyect.ApplyTo(modelo, ModelState);
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            Proyecto modelo2 = mapper.Map<Proyecto>(modelo);

            db.Autos.Update(modelo2);
            await db.SaveChangesAsync();
            return NoContent();
        }


    }
}
