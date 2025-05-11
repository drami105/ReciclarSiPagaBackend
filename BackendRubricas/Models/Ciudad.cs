using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }
        [Required(ErrorMessage = "IdCiudad es obligatoria")]
        [StringLength(100)]
        public string descripcion { get; set; }
    }
}
