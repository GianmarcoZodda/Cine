using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: yy/MM/dd}")]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(42, MinimumLength = 2, ErrorMessage = ErrorMsgs.MaxMin)]
        //"Taare Zameen Par: Every Child Is Special." Este título tiene un total de 42 caracteres, incluyendo espacios y signos de puntuación. La mas corta es "Up".
        public string Titulo { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = ErrorMsgs.MaxMin)]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public string Imagen { get; set; } = "default.jpg";

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int GeneroId { get; set; }

        public Genero Genero { get; set; }

        public List<Funcion> Funciones { get; set; }


    }
}