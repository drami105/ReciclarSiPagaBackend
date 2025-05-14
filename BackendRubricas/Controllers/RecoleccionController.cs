using BackendReciclarsipaga.Models;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoleccionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecoleccionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<RecoleccionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recoleccion>>> GetRecoleccion()
        {
            return await _context.recoleccion.ToListAsync();
        }

        // GET: api/<RecoleccionController>
        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<IEnumerable<RecoleccionDto>>> GetSolicitudes(int idUsuario)
        {
            try
            {
                var result = await _context.recoleccionDto
                    .FromSqlInterpolated($"EXEC SP_Solicitudes @idUsuario = {idUsuario}")
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error ejecutando el SP: {ex.Message}");
            }
        }



        // POST: api/<RecoleccionController>
        [HttpPost]
        public async Task<IActionResult> PostRecoleccion([FromBody] Recoleccion recoleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DateTime date = DateTime.Now;
                recoleccion.fechaSolicitud = date;
                _context.recoleccion.Add(recoleccion);
                await _context.SaveChangesAsync();
                return Ok(recoleccion);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "error guardando solicitud "+ ex.Message);
            }
        }


    }
}
