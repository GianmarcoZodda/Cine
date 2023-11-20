using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models.ViewModels
{
    public class RegistrarVM
    {

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsgs.StrLength)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsgs.StrLength)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(1000000, 99999999, ErrorMessage = ErrorMsgs.MaxMin)]
        public int DNI { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [EmailAddress(ErrorMessage = ErrorMsgs.EmailInvalido)] 
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMsgs.StrLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ErrorMsgs.PasswordIncorrecta)]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "No soy un Robot")]
        //public bool Captcha {  get; set; } = false
    }
}
