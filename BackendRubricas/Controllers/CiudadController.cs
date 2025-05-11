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
using BackendReciclarsipaga.Models;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CiudadController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/<CiudadController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudad()
        {
            return await _context.ciudad.ToListAsync();
        }

    }
}
