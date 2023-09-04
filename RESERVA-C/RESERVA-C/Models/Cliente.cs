namespace RESERVA_C.Models
{
    public class Cliente : Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Reserva> Reservas { get; set; }


    }
}