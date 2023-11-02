using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESERVA_C.Models
{
    public class Funcion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }

        [NotMapped]
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }//propiedad autoimplementada para que el get sea de la prop FechaHora

        [NotMapped]
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }//propiedad autoimplementada para que el get sea de la prop FechaHora

        [Required(ErrorMessage = ErrorMsgs.Required)]
        [StringLength(200, MinimumLength = 15, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Descripcion { get; set; }

        //Esto va a ser una propiedad computada teniendo en cuenta la sala de la funcion y las reservas de esta
        public int ButacasDisponibles { get; }

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


        // Resolver en la capa de servicios.
        public string FuncionCompleta
        { 
            get { 
                if(Pelicula != null && Sala != null)
                {
                    return $"{Pelicula.Titulo} - {FechaHora} - Sala: {Sala.Numero}";
                }
                return "N/D";
            } 
        }
    }
}