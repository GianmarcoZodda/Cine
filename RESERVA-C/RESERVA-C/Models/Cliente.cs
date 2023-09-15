using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Cliente : Persona
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
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
        public List<Reserva> Reservas { get; set; }


    }
}