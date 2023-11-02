using Microsoft.AspNetCore.Identity;
using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Persona : IdentityUser<int>
    {
       // public int Id { get; set; }


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
        [Range(100000000, 999999999, ErrorMessage = ErrorMsgs.MaxMin)]
        public int Telefono { get; set; }


        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = ErrorMsgs.StrLength)]
        public string Direccion { get; set; }


        //[Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = ErrorMsgs.StrLength)]
        [Display(Name = "Usuario")]
        public override string UserName {
            get { return base.UserName; }
            set { base.Email = value; } 
        }


        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMsgs.StrLength)]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "Password1!";


        [Required(ErrorMessage = ErrorMsgs.Required)]
        [EmailAddress(ErrorMessage = ErrorMsgs.EmailInvalido)]
        public override string Email {
            get { return base.Email; }
            set { base.Email = value; } 
        }


        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yy}")]
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; } 

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto
        {
            get
            {
                return $"{Apellido}, {Nombre}";
            }
        }

    }
}
