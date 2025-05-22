using BackendReciclarsipaga.Models;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<PersonaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersona()
        {
            return await _context.persona.ToListAsync();
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var personaConCiudad = await (
                from persona in _context.persona
                join barrio in _context.barrio on persona.idBarrio equals barrio.idBarrio
                join ciudad in _context.ciudad on barrio.idCiudad equals ciudad.idCiudad
                where persona.idPersona == id
                select new
                {
                    persona.idPersona,
                    persona.documento,
                    persona.idTipoDocumento,
                    persona.primerNombre,
                    persona.segundoNombre,
                    persona.primerApellido,
                    persona.segundoApellido,
                    persona.idBarrio,
                    persona.direccion,
                    persona.telefono,
                    idCiudad = ciudad.idCiudad,
                    descripcionCiudad = ciudad.descripcion
                }
            ).FirstOrDefaultAsync();

            if (personaConCiudad == null)
            {
                return NotFound();
            }

            return Ok(personaConCiudad);
        }


        // POST: api/<PersonaController>
        [HttpPost()]
        public async Task<IActionResult> PostPersona([FromBody] PersonaUsuarioDto dto)
        {
            if (UsuarioExist(dto.Persona.documento))
            {
                return BadRequest("Usuario ya está registrado en el sistema.");
            }
            else
            {
                if (DocumentoExist(dto.Persona.documento))
                {
                    _context.Entry(dto.Persona).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(dto.Persona);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return StatusCode(500, "Error actualizando el Usuario.");
                    }
                }
                else
                {
                    try
                    {
                        _context.persona.Add(dto.Persona);
                        await _context.SaveChangesAsync();//Guardo la persona primero

                        int personaId = dto.Persona.idPersona;//Obtengo ID

                        dto.Usuario.idPersona = personaId;//Asignar el ID a Usuario
                        dto.Usuario.idPerfil = 1;//Por defecto todos los usuarios quedan con perfil 1

                        _context.usuario.Add(dto.Usuario);
                        await _context.SaveChangesAsync();//Guarda el Usuario

                        return Ok(dto);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return StatusCode(500, "Error creando el Usuario.");
                    }

                }

            }
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, [FromBody] Persona persona)
        {
            if (id != persona.idPersona)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");
            }

            var personaExistente = await _context.persona.FindAsync(id);
            if (personaExistente == null)
            {
                return NotFound();
            }

            // Actualizar propiedades
            personaExistente.direccion = persona.direccion;
            personaExistente.idBarrio = persona.idBarrio;
            personaExistente.telefono = persona.telefono;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.persona.Any(e => e.idPersona == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); 
        }

        [HttpPut("{id}/{idPerfil}")]
        public async Task<IActionResult> PutPersonaCompleto(int id, int idPerfil, [FromBody] Persona dto)
        {
            if (id != dto.idPersona)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");
            }

            var personaExistente = await _context.persona.FindAsync(id);
            if (personaExistente == null)
            {
                return NotFound("Persona no encontrada.");
            }

            // Buscar usuario relacionado
            var usuarioExistente = await _context.usuario
                .FirstOrDefaultAsync(u => u.idPersona == personaExistente.idPersona);

            if (usuarioExistente == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Actualizar propiedades de persona
            personaExistente.primerNombre = dto.primerNombre;
            personaExistente.segundoNombre = dto.segundoNombre;
            personaExistente.primerApellido = dto.primerApellido;
            personaExistente.segundoApellido = dto.segundoApellido;
            personaExistente.direccion = dto.direccion;
            personaExistente.documento = dto.documento;
            personaExistente.telefono = dto.telefono;
            personaExistente.correo = dto.correo;
            personaExistente.idBarrio = dto.idBarrio;

            // Actualizar perfil del usuario
            usuarioExistente.idPerfil = idPerfil;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DocumentoExist(int documento)
        {
            return _context.persona.Any(e => e.documento == documento);
        }

        private bool UsuarioExist(int documento)
        {
            return (from u in _context.usuario
                    join p in _context.persona on u.idPersona equals p.idPersona
                    where p.documento == documento
                    select u).Any();
        }

    }
}
