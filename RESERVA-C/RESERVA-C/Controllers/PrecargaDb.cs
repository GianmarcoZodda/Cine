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
                FechaAlta = new DateTime(2002, 10, 03)
            };

            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

    
    }
}
