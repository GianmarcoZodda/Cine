using System.ComponentModel.DataAnnotations;
using RESERVA_C.Helpers;
namespace RESERVA_C.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(1, 20, ErrorMessage = ErrorMsgs.MaxMin)]
        public int Numero { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int TipoSalaId { get; set; }
        public TipoSala TipoSala { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(20, int.MaxValue, ErrorMessage = ErrorMsgs.Min)]
        public int CapacidadButacas { get; set; }

        public List<Funcion> Funciones { get; set; }

    }
}
