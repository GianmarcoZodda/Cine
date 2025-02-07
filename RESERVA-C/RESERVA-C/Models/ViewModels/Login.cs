﻿using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models.ViewModels
{
    public class Login
    {

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [EmailAddress(ErrorMessage = ErrorMsgs.EmailInvalido)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMsgs.StrLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        public bool Recordarme { get; set; } = false;
    }
}
