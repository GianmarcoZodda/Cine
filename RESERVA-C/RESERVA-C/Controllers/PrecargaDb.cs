using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Data;
using RESERVA_C.Models;

namespace RESERVA_C.Controllers
{
    public class PrecargaDb : Controller
    {

        private readonly ReservaContext _context;

        public PrecargaDb(ReservaContext context)
        {
            this._context = context;
        }

        public IActionResult Seed()
        {
            if (!_context.Personas.Any())
            {
                this.AddPersonas();
            }

            if (!_context.Generos.Any())
            {
                this.AddGeneros();
            }

            if (!_context.Peliculas.Any())
            {
                this.AddPeliculas();
            }

            return RedirectToAction("Index", "Home", new {mensaje = "Hice Precarga"});
        }

        public IActionResult Remove()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return RedirectToAction("Index", "Home", new {mensaje = "Borre"});
        }
        private void AddPersonas()
        {
            Persona persona = new Persona()
            {
                Nombre = "Gianmarco",
                Apellido = "Zodda",
                DNI = 44211766,
                Telefono = 100000001,
                Direccion = "san martin",
                UserName = "gjzodda",
                Password = "Password1",
                Email = "zoddagj@gmail.com",
                FechaAlta = DateTime.Now
            };

            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        private void AddGeneros() 
        {
            Genero genero = new Genero()
            {
                Nombre = "Ciencia Ficcion"
            };
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }

        private void AddPeliculas()
        {
            Pelicula pelicula = new Pelicula()
            {
                FechaLanzamiento = new DateTime(14,04,2006),
                Titulo = "Piratas del Caribe",
                Descripcion = "Aparece Jack Sparrow y se pelea con el que tiene la pata de palo",
                GeneroId = BuscarGenero("Ciencia Ficcion")
            };
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();
        }

        private int BuscarGenero(string nombre) 
        {
            int generoId = 1;
            Genero genero = _context.Generos.FirstOrDefault(g => g.Nombre == nombre);
            if(genero != null)
            {
                generoId = genero.Id;
            }
            return generoId;
        }


    }
}
