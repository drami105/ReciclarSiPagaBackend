using BackendReciclarsipaga.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public interface IBarrioService
    {
        Task<IEnumerable<Barrio>> GetAllBarriosAsync();
    }
}
