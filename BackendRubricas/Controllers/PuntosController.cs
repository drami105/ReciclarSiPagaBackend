using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Authorization;
using BackendReciclarsipaga.Models;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PuntosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Puntos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puntos>>> GetPuntos()
        {
            return await _context.puntos.ToListAsync();
        }

        // GET: api/Puntos/usuario/5
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<int>> GetPuntosPorUsuario(long idUsuario)
        {
            var puntos = await _context.puntos
                .Where(p => p.idUsuario == idUsuario)
                .Select(p => p.puntos) 
                .FirstOrDefaultAsync();

            if (puntos == default)
            {
                return NotFound();
            }

            return Ok(puntos);
        }


    }
}
