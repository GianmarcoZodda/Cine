using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{ 
public class Reserva
{

    [Key]
    public  int Id { get; set; }

    public Funcion Funcion { get; set; }

    [Required(ErrorMessage = ErrorMsgs.Required)]
    [DataType(DataType.Date)]
    public DateTime FechaAlta { get; set; }

    public Cliente Cliente { get; set; }

    [Required(ErrorMessage = ErrorMsgs.Required)]
    public int CantidadButacas { get; set; }


}
}