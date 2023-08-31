using System;

public class Reserva
{
    public  int Id { get; set; }

    public Funcion Funcion { get; set; }

    public DateTime FechaAlta { get; set; }

    public Cliente Cliente { get; set; }

    public int CantidadButacas { get; set; }


}
