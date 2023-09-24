 using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = ErrorMsgs.MaxMin)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Password { get; set; } = "Password1!";

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [EmailAddress(ErrorMessage = ErrorMsgs.EmailInvalido)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: yy/MM/dd}")]
        public DateTime FechaAlta { get; set; }

    }
}
