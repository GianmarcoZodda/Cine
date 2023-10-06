using RESERVA_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RESERVA_C.Models
{
    public class Cliente : Persona
    {
       public List<Reserva> Reservas { get; set; }


    }
}