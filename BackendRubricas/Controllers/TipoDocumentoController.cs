using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendRubricas.Context;
using BackendRubricas.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendRubricas.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoDocumentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetTipoDocumento()
        {
            return await _context.tipoDocumento.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumento>> GetTipoDocumento(int id)
        {
            var tipoDocumento = await _context.tipoDocumento.FindAsync(id);

            if (tipoDocumento == null)
            {
                return NotFound();
            }

            return tipoDocumento;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDocumento(int id, TipoDocumento tipoDocumento)
        {
            if (id != tipoDocumento.idTipoDocumento)
            {
                return BadRequest();
            }

            _context.Entry(tipoDocumento).State = EntityState.Modified;

            if (TipoDocumentoExisDesc(tipoDocumento.Descripcion))
            {
                return BadRequest("Ya existe tipo de documento con esta descripción");
            }
            else
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDocumentoExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NoContent();
        }



        private bool TipoDocumentoExist(int id)
        {
            return _context.tipoDocumento.Any(e => e.idTipoDocumento == id);
        }

        private bool TipoDocumentoExisDesc(string tipoDocumento)
        {
            return _context.tipoDocumento.Any(e => e.Descripcion == tipoDocumento);
        }
    }
}
