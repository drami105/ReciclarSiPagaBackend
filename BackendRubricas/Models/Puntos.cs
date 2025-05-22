
using System.ComponentModel.DataAnnotations;


namespace BackendReciclarsipaga.Models
{
    public class Puntos
    {
        [Key]
        public int idPuntos { get; set; }
        public int idUsuario { get; set; }
        public long puntos { get; set; }

    }
}
