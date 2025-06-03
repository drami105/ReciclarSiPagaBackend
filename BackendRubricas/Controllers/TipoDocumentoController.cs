using BackendRubricas.Models;
using BackendRubricas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendRubricas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _service;

        public TipoDocumentoController(ITipoDocumentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetTipoDocumento()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumento>> GetTipoDocumento(int id)
        {
            var tipo = await _service.GetByIdAsync(id);
            if (tipo == null)
                return NotFound();

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDocumento(int id, TipoDocumento tipoDocumento)
        {
            if (id != tipoDocumento.idTipoDocumento)
                return BadRequest();

            var success = await _service.UpdateAsync(id, tipoDocumento);

            if (!success)
                return BadRequest("Error actualizando. Puede que el tipo no exista o la descripción esté duplicada.");

            return NoContent();
        }
    }
}
