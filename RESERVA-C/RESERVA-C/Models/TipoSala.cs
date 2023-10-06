using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;
namespace RESERVA_C.Models
{
    public class TipoSala
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = ErrorMsgs.Required)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = ErrorMsgs.MaxMin)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = ErrorMsgs.Required)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMsgs.MaxMin)]
        public int Precio { get; set; }

        public List<Sala> Salas { get; set; }

    }
}
