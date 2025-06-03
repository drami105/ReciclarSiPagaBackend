using BackendReciclarsipaga.Models;
using BackendReciclarsipaga.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoleccionController : ControllerBase
    {
        private readonly IRecoleccionService _recoleccionService;

        public RecoleccionController(IRecoleccionService recoleccionService)
        {
            _recoleccionService = recoleccionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recoleccion>>> GetRecoleccion()
        {
            var recolecciones = await _recoleccionService.GetAllAsync();
            return Ok(recolecciones);
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<IEnumerable<RecoleccionDto>>> GetSolicitudes(int idUsuario)
        {
            try
            {
                var result = await _recoleccionService.GetSolicitudesAsync(idUsuario);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error ejecutando el procedimiento almacenado.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostRecoleccion([FromBody] Recoleccion recoleccion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nueva = await _recoleccionService.CreateAsync(recoleccion);
                return Ok(nueva);
            }
            catch
            {
                return StatusCode(500, "Error guardando la solicitud.");
            }
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<double>> GetKilogramosConfPorUsuario(long idUsuario)
        {
            var totalKg = await _recoleccionService.GetKilogramosConfirmadosPorUsuarioAsync(idUsuario);
            return Ok(totalKg);
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> GetRecoleccionesPendientes()
        {
            var result = await _recoleccionService.GetSolicitudesPendientesConDetalleAsync();
            return Ok(result);
        }

        [HttpPut("confirmar")]
        public async Task<IActionResult> ConfirmarRecoleccion([FromBody] ActualizarRecoleccionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizada = await _recoleccionService.UpdateAsync(dto);

                if (actualizada == null)
                    return NotFound("No se encontró la solicitud.");

                return Ok(actualizada);
            }
            catch
            {
                return StatusCode(500, "Error actualizando la solicitud.");
            }
        }


    }
}
