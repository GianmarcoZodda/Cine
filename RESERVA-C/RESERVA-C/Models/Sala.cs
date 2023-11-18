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
        [Display(Name = "Numero de Sala")]
        public int Numero { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Tipo de Sala")]
        public int TipoSalaId { get; set; }

        [Display(Name = "Tipo de Sala")]
        public TipoSala TipoSala { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(20, int.MaxValue, ErrorMessage = ErrorMsgs.Min)]
        [Display(Name = "Capacidad de Butacas")]
        public int CapacidadButacas { get; set; }

        public List<Funcion> Funciones { get; set; }

    }
}
