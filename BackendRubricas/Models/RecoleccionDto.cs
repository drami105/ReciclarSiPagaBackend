using System;
using System.ComponentModel.DataAnnotations;

namespace BackendReciclarsipaga.Models
{
    public class RecoleccionDto
    {
        public int idSolicitud { get; set; }

        public int idUsuario { get; set; }

        public decimal? kilogramosIni { get; set; }

        [StringLength(500)]
        public string? observacion { get; set; }

        public DateTime? fechaSolicitud { get; set; }

        public bool estado { get; set; }

        public DateTime? fechaRecoleccion { get; set; }

        public decimal? kilogramosConf { get; set; }

        public int? idUsuarioRecole { get; set; }

        [StringLength(200)]
        public string? usuarioRecole { get; set; }

    }
}
