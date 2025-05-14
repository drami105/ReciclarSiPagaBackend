using BackendReciclarsipaga.Models;
using BackendRubricas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackendReciclarsipaga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        { 
            return await _context.usuario.ToListAsync();
        }

        // POST: api/<UsuarioController>
        [HttpPost("Autenticacion")]
        public IActionResult Autenticacion([FromBody] LoginDto login)
        {
            var resultado = (from u in _context.usuario
                             join p in _context.persona on u.idPersona equals p.idPersona
                             where p.correo == login.Correo && u.contrasena == login.Contrasena
                             select new
                             {
                                 NombreCompleto = string.Join(" ", p.primerNombre, p.segundoNombre, p.primerApellido, p.segundoApellido).Trim(),
                                 Direccion = p.direccion,
                                 Documento = p.documento,
                                 Telefono = p.telefono,
                                 Correo = p.correo,
                                 IdBarrio = p.idBarrio,
                                 IdPerfil = u.idPerfil,
                                 IdUsuario = p.idPersona
                             }).FirstOrDefault();

            if (resultado == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(resultado);
        }


    }
}
