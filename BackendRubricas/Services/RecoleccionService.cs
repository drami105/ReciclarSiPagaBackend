using BackendRubricas.Context;
using BackendReciclarsipaga.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class RecoleccionService : IRecoleccionService
    {
        private readonly AppDbContext _context;

        public RecoleccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recoleccion>> GetAllAsync()
        {
            return await _context.recoleccion.ToListAsync();
        }

        public async Task<IEnumerable<RecoleccionDto>> GetSolicitudesAsync(int idUsuario)
        {
            return await _context.recoleccionDto
                .FromSqlInterpolated($"EXEC SP_Solicitudes @idUsuario = {idUsuario}")
                .ToListAsync();
        }

        public async Task<Recoleccion> CreateAsync(Recoleccion recoleccion)
        {
            recoleccion.fechaSolicitud = DateTime.Now;
            _context.recoleccion.Add(recoleccion);
            await _context.SaveChangesAsync();
            return recoleccion;
        }

        public async Task<double> GetKilogramosConfirmadosPorUsuarioAsync(long idUsuario)
        {
            return await _context.recoleccion
                .Where(r => r.idUsuario == idUsuario)
                .SumAsync(r => (double?)r.kilogramosConf) ?? 0;
        }

        public async Task<IEnumerable<object>> GetSolicitudesPendientesConDetalleAsync()
        {
            var resultados = await (from r in _context.recoleccion
                                    join u in _context.usuario on r.idUsuario equals u.idUsuario
                                    join p in _context.persona on u.idPersona equals p.idPersona
                                    where r.estado == false
                                    select new
                                    {
                                        r.idSolicitud,
                                        r.idUsuario,
                                        r.fechaSolicitud,
                                        r.kilogramosIni,
                                        r.estado,
                                        Documento = p.documento,
                                        NombreCompleto = string.Join(" ", p.primerNombre, p.segundoNombre, p.primerApellido, p.segundoApellido).Trim()
                                    }).ToListAsync();

            return resultados;
        }

        public async Task<Recoleccion?> UpdateAsync(ActualizarRecoleccionDto dto)
        {
            var recoleccion = await _context.recoleccion.FindAsync(dto.IdSolicitud);

            if (recoleccion == null)
                return null;

            recoleccion.kilogramosConf = dto.KilogramosConf;
            recoleccion.idUsuarioRecole = dto.IdUsuarioRecole;
            recoleccion.fechaRecoleccion = DateTime.Now;
            recoleccion.estado = true;

            await _context.SaveChangesAsync();

            return recoleccion;
        }


    }
}
