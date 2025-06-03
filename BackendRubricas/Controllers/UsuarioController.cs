using BackendReciclarsipaga.Models;
using BackendReciclarsipaga.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost("Autenticacion")]
        public async Task<IActionResult> Autenticacion([FromBody] LoginDto login)
        {
            var resultado = await _usuarioService.AutenticarAsync(login);
            if (resultado == null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(resultado);
        }

        [HttpGet("detalle")]
        public async Task<IActionResult> GetUsuariosConDetalle()
        {
            var usuariosDetalle = await _usuarioService.GetUsuariosConDetalleAsync();
            return Ok(usuariosDetalle);
        }
    }
}
