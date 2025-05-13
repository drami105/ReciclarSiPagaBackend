using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class Persona
    {
        [Key]
        public int idPersona { get; set; }
        //[Required(ErrorMessage = "IdPersona es obligatoria")]
        public int documento { get; set; }
        public int idTipoDocumento { get; set; }
        [StringLength(50)]
        public string primerNombre { get; set; }
        [StringLength(50)]
        public string segundoNombre { get; set; }
        [StringLength(50)]
        public string primerApellido { get; set; }
        [StringLength(50)]
        public string segundoApellido { get; set; }
        [StringLength(100)]
        public string correo { get; set; }
        public long telefono { get; set; }
        [StringLength(200)]
        public string direccion { get; set; }
        public int idBarrio { get; set; }
    }
}
