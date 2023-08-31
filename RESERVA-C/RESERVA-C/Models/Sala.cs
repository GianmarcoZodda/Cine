using System;

public class Sala
{

    public int Id { get; set; }

    public int Numero { get; set; }
    
    public string TipoSala { get; set; }
    
    public int CapacidadButacas { get; set; }
    
    public List<Funcion> Funciones { get; set; }

}
