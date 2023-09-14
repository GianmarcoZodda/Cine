﻿using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Funcion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(200, MinimumLength = 15, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Descripcion { get; set; }

        //Esto va a ser una propiedad computada teniendo en cuenta la sala de la funcion y las reservas de esta
        public int ButacasDisponibles { get; }

        public bool Confirmada { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public List<Reserva> Reservas { get; set; }

    }
}