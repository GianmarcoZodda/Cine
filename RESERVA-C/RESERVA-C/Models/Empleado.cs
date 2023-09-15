using System.ComponentModel.DataAnnotations;
using RESERVA_C.Helpers;

namespace RESERVA_C.Models
{
    public class Empleado : Persona
    {
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(10000000, 99999999, ErrorMessage = ErrorMsgs.MaxMin)]
        public int DNI { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(100000000, 999999999, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Legajo { get; set; }

    }
}