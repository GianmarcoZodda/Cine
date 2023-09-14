using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Nombre { get; set; }

        public List<Pelicula> Peliculas { get; set; }
    }
}
