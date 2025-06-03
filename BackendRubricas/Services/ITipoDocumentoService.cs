using BackendRubricas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendRubricas.Services
{
    public interface ITipoDocumentoService
    {
        Task<IEnumerable<TipoDocumento>> GetAllAsync();
        Task<TipoDocumento?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, TipoDocumento tipoDocumento);
        Task<bool> ExistsByIdAsync(int id);
        Task<bool> ExistsByDescripcionAsync(string descripcion);
    }
}
