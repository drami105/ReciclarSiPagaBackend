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
