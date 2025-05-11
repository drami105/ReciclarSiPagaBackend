using BackendReciclarsipaga.Models;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarrioController : ControllerBase
    {

        private readonly AppDbContext _context;

        public BarrioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<BarrioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barrio>>> GetBarrio()
        {
            return await _context.barrio.ToListAsync();
        }

    }
}
