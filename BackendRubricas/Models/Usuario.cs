using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class Usuario
    {

        [Key]
        public int idUsuario { get; set; }
        //[Required(ErrorMessage = "IdUsuario es obligatoria")]
        [StringLength(200)]
        public string contrasena { get; set; }
        public int idPerfil { get; set; }
        public int idPersona { get; set; }

       
    }
}
