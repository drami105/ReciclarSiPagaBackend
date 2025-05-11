using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class Barrio
    {
        [Key]
        public int IdBarrio { get; set; }
        [Required(ErrorMessage = "IdBarrio es obligatoria")]
        [StringLength(100)]
        public string descripcion { get; set; }
        public int IdCiudad { get; set; }
    }
}
