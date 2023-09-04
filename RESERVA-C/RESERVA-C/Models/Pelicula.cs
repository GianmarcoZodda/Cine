namespace RESERVA_C.Models
{
    public class Pelicula
    {

        public int Id { get; set; }

        public DateTime FechaLanzamiento { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public Genero Genero { get; }

        public List<Funcion> Funciones { get; set; }


    }
}