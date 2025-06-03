using BackendReciclarsipaga.Models;
using BackendRubricas.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class BarrioService : IBarrioService
    {
        private readonly AppDbContext _context;

        public BarrioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Barrio>> GetAllBarriosAsync()
        {
            return await _context.barrio.ToListAsync();
        }
    }
}
