using BackendReciclarsipaga.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public interface IPuntosService
    {
        Task<IEnumerable<Puntos>> GetAllAsync();
        Task<int?> GetPuntosPorUsuarioAsync(long idUsuario);
        Task<bool> AgregarPuntosAsync(int idUsuario, long puntosNuevos);
        Task<bool> DisminuirPuntosAsync(int idUsuario, long puntosNuevos);

    }
}
