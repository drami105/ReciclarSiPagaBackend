using System;
using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class Recoleccion
    {
        [Key]
        public int idSolicitud { get; set; }

        [Required(ErrorMessage = "El idUsuario es obligatorio.")]
        public int idUsuario { get; set; }

        public decimal? kilogramosIni { get; set; }

        [StringLength(500)]
        public string? observacion { get; set; }

        public DateTime? fechaSolicitud { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool estado { get; set; }

        public DateTime? fechaRecoleccion { get; set; }

        public decimal? kilogramosConf { get; set; }

        public int? idUsuarioRecole { get; set; }
    }
}
