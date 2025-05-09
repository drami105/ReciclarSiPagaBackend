using System.ComponentModel.DataAnnotations;

namespace BackendRubricas.Models
{
    public class TipoDocumento
    {
        [Key]
        public int idTipoDocumento { get; set; }
        [Required(ErrorMessage = "Categoria es obligatoria")]
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
