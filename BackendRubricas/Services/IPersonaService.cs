using BackendReciclarsipaga.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public interface IPersonaService
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<object> GetWithCiudadAsync(int id);
        Task<object> CreateOrUpdatePersonaAsync(PersonaUsuarioDto dto);
        Task<bool> UpdatePersonaAsync(int id, Persona persona);
        Task<bool> UpdatePersonaCompletaAsync(int id, int idPerfil, Persona persona);
    }
}
