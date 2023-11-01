using System.ComponentModel.DataAnnotations;
using RESERVA_C.Helpers;

namespace RESERVA_C.Models
{
    public class Empleado : Persona
    {
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Legajo { get; set; } = Generadores.GetNewLegajo(5);

    }
}