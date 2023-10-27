using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RESERVA_C.Models.ViewModels
{
    public class FuncionIndexVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }

        [NotMapped]
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        public DateTime Fecha
        {
            get { return FechaHora.Date; } // Obtiene solo la fecha de FechaHora
            set { FechaHora = value.Date + FechaHora.TimeOfDay; } // Establece la fecha en FechaHora sin cambiar la hora
        }

        [NotMapped]
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Time)]
        public DateTime Hora
        {
            get { return DateTime.MinValue.Add(FechaHora.TimeOfDay); ; } // Obtiene solo la hora de FechaHora
            set { FechaHora = FechaHora.Date + value.TimeOfDay; } // Establece la hora en FechaHora sin cambiar la fecha
        }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(200, MinimumLength = 15, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Descripcion { get; set; }

        //Esto va a ser una propiedad computada teniendo en cuenta la sala de la funcion y las reservas de esta
        [Display(Name = "Butacas Disponibles")]
        public int ButacasDisponibles { get; set; }

        public bool Confirmada { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Pelicula")]
        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Sala")]
        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public List<Reserva> Reservas { get; set; }

        public string FuncionCompleta
        { get { return $"{Pelicula.Titulo} - {FechaHora} - Sala: {Sala.Numero}"; } }
    }
}
