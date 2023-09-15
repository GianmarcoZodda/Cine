using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int Numero { get; set; }

        public TipoSala TipoSala { get; set; }

        [Required(ErrorMessage = ErrorMsgs.Required)]
        public int CapacidadButacas { get; set; }

        public List<Funcion> Funciones { get; set; }

    }
}
