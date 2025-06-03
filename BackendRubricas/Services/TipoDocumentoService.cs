using BackendRubricas.Context;
using BackendRubricas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendRubricas.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly AppDbContext _context;

        public TipoDocumentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoDocumento>> GetAllAsync()
        {
            return await _context.tipoDocumento.ToListAsync();
        }

        public async Task<TipoDocumento?> GetByIdAsync(int id)
        {
            return await _context.tipoDocumento.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, TipoDocumento tipoDocumento)
        {
            if (id != tipoDocumento.idTipoDocumento)
                return false;

            if (await _context.tipoDocumento.AnyAsync(e => e.descripcion == tipoDocumento.descripcion))
                return false;

            _context.Entry(tipoDocumento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsByIdAsync(id))
                    return false;
                throw;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.tipoDocumento.AnyAsync(e => e.idTipoDocumento == id);
        }

        public async Task<bool> ExistsByDescripcionAsync(string descripcion)
        {
            return await _context.tipoDocumento.AnyAsync(e => e.descripcion == descripcion);
        }
    }
}
