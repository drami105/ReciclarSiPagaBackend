using BackendReciclarsipaga.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public interface IRecoleccionService
    {
        Task<IEnumerable<Recoleccion>> GetAllAsync();
        Task<IEnumerable<RecoleccionDto>> GetSolicitudesAsync(int idUsuario);
        Task<Recoleccion> CreateAsync(Recoleccion recoleccion);
        Task<double> GetKilogramosConfirmadosPorUsuarioAsync(long idUsuario);
        Task<IEnumerable<object>> GetSolicitudesPendientesConDetalleAsync();
        Task<Recoleccion?> UpdateAsync(ActualizarRecoleccionDto dto);


    }
}
