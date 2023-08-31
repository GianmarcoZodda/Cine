using System;

public class Funcion
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime Hora { get; set; }

    public string Descripcion { get; set; }

    public int ButacasDisponibles { get; set; }

    public bool Confirmada { get; set; }

    public Pelicula Pelicula { get; set; }

    public Sala Sala { get; set; }

    public List<Reserva> Reservas { get; set; }



}
