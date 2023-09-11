 using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 1, ErrorMsgs.MaxMin)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(30, MinimumLength = 8, ErrorMsgs.MaxMin)]
        public string Password { get; set; } = "Password1!";

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]+$", ErrorMsgs.EmailInvalido)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: yy/MM/dd}")]
        public DateTime FechaAlta { get; set; }

    }
}
