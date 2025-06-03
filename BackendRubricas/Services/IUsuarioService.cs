using BackendReciclarsipaga.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<object?> AutenticarAsync(LoginDto login);
        Task<IEnumerable<object>> GetUsuariosConDetalleAsync();
    }
}
