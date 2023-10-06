using System.ComponentModel.DataAnnotations;
using RESERVA_C.Helpers;
namespace RESERVA_C.Models
{ 
public class Reserva
{

    [Key]
    public  int Id { get; set; }


    [Required(ErrorMessage = ErrorMsgs.Required)]
    public int FuncionId { get; set; }
    public Funcion Funcion { get; set; }

    [Required(ErrorMessage = ErrorMsgs.Required)]
    public DateTime FechaAlta { get; set; }

    [Required(ErrorMessage = ErrorMsgs.Required)]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    [Required(ErrorMessage = ErrorMsgs.Required)]
    [Range(1, 50, ErrorMessage = ErrorMsgs.MaxMin)]//*nahuel* puse 50 como ejemplo
    public int CantidadButacas { get; set; }


}
}