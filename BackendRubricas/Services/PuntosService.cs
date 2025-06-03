using BackendRubricas.Context;
using BackendReciclarsipaga.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class PuntosService : IPuntosService
    {
        private readonly AppDbContext _context;

        public PuntosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Puntos>> GetAllAsync()
        {
            return await _context.puntos.ToListAsync();
        }

        public async Task<int?> GetPuntosPorUsuarioAsync(long idUsuario)
        {
            var puntos = await _context.puntos
                .Where(p => p.idUsuario == idUsuario)
                .Select(p => (int?)p.puntos) // nullable
                .FirstOrDefaultAsync();

            return puntos;
        }

        public async Task<bool> AgregarPuntosAsync(int idUsuario, long puntosNuevos)
        {
            var registro = await _context.puntos.FirstOrDefaultAsync(p => p.idUsuario == idUsuario);

            if (registro == null)
                return false;

            registro.puntos += puntosNuevos;

            _context.puntos.Update(registro);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DisminuirPuntosAsync(int idUsuario, long puntosNuevos)
        {
            var registro = await _context.puntos.FirstOrDefaultAsync(p => p.idUsuario == idUsuario);

            if (registro == null)
                return false;

            registro.puntos -= puntosNuevos;

            _context.puntos.Update(registro);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
