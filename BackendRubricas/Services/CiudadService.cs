using BackendRubricas.Context;
using BackendReciclarsipaga.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly AppDbContext _context;

        public CiudadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ciudad>> GetAllCiudadesAsync()
        {
            return await _context.ciudad.ToListAsync();
        }
    }
}
