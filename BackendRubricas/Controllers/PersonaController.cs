using BackendReciclarsipaga.Models;
using BackendReciclarsipaga.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersona()
        {
            var personas = await _personaService.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var result = await _personaService.GetWithCiudadAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostPersona([FromBody] PersonaUsuarioDto dto)
        {
            var result = await _personaService.CreateOrUpdatePersonaAsync(dto);

            if (result is string error)
                return BadRequest(error);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, [FromBody] Persona persona)
        {
            if (id != persona.idPersona)
                return BadRequest("ID mismatch");

            var updated = await _personaService.UpdatePersonaAsync(id, persona);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/{idPerfil}")]
        public async Task<IActionResult> PutPersonaCompleto(int id, int idPerfil, [FromBody] Persona dto)
        {
            var updated = await _personaService.UpdatePersonaCompletaAsync(id, idPerfil, dto);
            if (!updated) return NotFound();

            return NoContent();
        }
    }
}
