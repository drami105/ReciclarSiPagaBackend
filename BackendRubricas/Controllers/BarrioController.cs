using BackendReciclarsipaga.Models;
using BackendReciclarsipaga.Services;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarrioController : ControllerBase
    {
        private readonly IBarrioService _barrioService;

        public BarrioController(IBarrioService barrioService)
        {
            _barrioService = barrioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barrio>>> GetBarrio()
        {
            var barrios = await _barrioService.GetAllBarriosAsync();
            return Ok(barrios);
        }
    }
}
