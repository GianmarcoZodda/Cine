using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Pelicula
    {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: yy/MM/dd}")]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(42, MinimumLength = 2, ErrorMsgs.MaxMin)]
        //"Taare Zameen Par: Every Child Is Special." Este título tiene un total de 42 caracteres, incluyendo espacios y signos de puntuación. La mas corta es "Up".
        public string Titulo { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(100, MinimumLength =10, ErrorMsgs.MaxMin)]
        public string Descripcion { get; set; }

        public int GeneroId { get; set; }

        public Genero Genero { get; set; }

        public List<Funcion> Funciones { get; set; }


    }
}