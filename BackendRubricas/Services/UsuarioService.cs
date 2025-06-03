using BackendRubricas.Context;
using BackendReciclarsipaga.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.usuario.ToListAsync();
        }

        public async Task<object?> AutenticarAsync(LoginDto login)
        {
            return await (from u in _context.usuario
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
                              IdUsuario = u.idUsuario,
                              IdPersona = p.idPersona

                          }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> GetUsuariosConDetalleAsync()
        {
            return await (from u in _context.usuario
                          join p in _context.persona on u.idPersona equals p.idPersona
                          join r in _context.tipoDocumento on p.idTipoDocumento equals r.idTipoDocumento
                          select new
                          {
                              NombreCompleto = string.Join(" ", p.primerNombre, p.segundoNombre, p.primerApellido, p.segundoApellido).Trim(),
                              PrimerNombre = p.primerNombre,
                              SegundoNombre = p.segundoNombre,
                              PrimerApellido = p.primerApellido,
                              SegundoApellido = p.segundoApellido,
                              Direccion = p.direccion,
                              Documento = p.documento,
                              Telefono = p.telefono,
                              Correo = p.correo,
                              IdBarrio = p.idBarrio,
                              IdPerfil = u.idPerfil,
                              IdPersona = p.idPersona,
                              IdUsuario = u.idUsuario,
                              IdTipoDocumento = p.idTipoDocumento,
                              TipoDocumento = r.descripcion
                          }).ToListAsync();
        }
    }
}
