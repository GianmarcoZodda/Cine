using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Data;
using RESERVA_C.Helpers;
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
            if (!_context.Clientes.Any())
            {
                this.AddClientes();
            }

            if (!_context.Empleados.Any())
            {
                this.AddEmpleados();
            }

            if (!_context.Generos.Any())
            {
                this.AddGeneros();
            }

            if (!_context.Peliculas.Any())
            {
                this.AddPeliculas();
            }

            if (!_context.TipoSalas.Any())
            {
                this.AddTipoSalas();
            }

            if (!_context.Salas.Any())
            {
                this.AddSalas();
            }

            if (!_context.Funciones.Any())
            {
                this.AddFunciones();
            }

            if (!_context.Reservas.Any())
            {
                this.AddReservas();
            }
            
            return RedirectToAction("Index", "Home", new {mensaje = "Hice Precarga"});
        }

        public IActionResult Remove()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return RedirectToAction("Index", "Home", new {mensaje = "Borre"});
        }
        private void AddClientes()
        {
            Persona cliente1 = new Cliente()
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
            _context.Personas.Add(cliente1);
            _context.SaveChanges();
        }
        private void AddEmpleados()
        {
            Persona empleado1 = new Empleado()
            {
                Nombre = "Nahuel",
                Apellido = "David",
                DNI = 43035648,
                Telefono = 100000001,
                Direccion = "Montecastro",
                UserName = "Nahueze",
                Password = "Password1",
                Email = "nahueze@gmail.com",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            _context.Personas.Add(empleado1);
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
                FechaLanzamiento = new DateTime(2006,04,14),
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
        private void AddTipoSalas()
        {
            TipoSala tipoSala1 = new TipoSala()
            {
                Nombre = "3D",
                Precio = 50
            };
            _context.TipoSalas.Add(tipoSala1);
            _context.SaveChanges();
        }

        private void AddSalas()
        {
            Sala sala1 = new Sala() 
            {
                Numero = 12,
                CapacidadButacas = 100,
                TipoSalaId = BuscarTipoSala("3D"),
            };
            _context.Salas.Add(sala1);
            _context.SaveChanges();
        }
        private int BuscarTipoSala(string nombre)
        {
            int tipoSalaId = 1;
            TipoSala tipoSala = _context.TipoSalas.FirstOrDefault(ts => ts.Nombre == nombre);
            if (tipoSala != null)
            {
                tipoSalaId = tipoSala.Id;
            }
            return tipoSalaId;
        }

        private void AddFunciones()
        {
            Funcion funcion1 = new Funcion()
            {
                FechaHora = new DateTime(2023, 11, 04, 12, 50, 00),
                Descripcion = "Funcion unica e inigualable",
                PeliculaId = BuscarPelicula("Piratas del Caribe"),
                SalaId = BuscarSala(12)
            };
            _context.Funciones.Add(funcion1);
            _context.SaveChanges();
        }

        private int BuscarSala(int numeroSala)
        {
            int salaId = 1;
            Sala sala = _context.Salas.FirstOrDefault(s => s.Numero == numeroSala);
            if (sala != null)
            {
                salaId = sala.Id;
            }
            return salaId;
        }

        private int BuscarPelicula(string titulo)
        {
            int peliculaId = 1;
            Pelicula pelicula = _context.Peliculas.FirstOrDefault(p => p.Titulo == titulo);
            if (pelicula != null)
            {
                peliculaId = pelicula.Id;
            }
            return peliculaId;
        }
        
        private void AddReservas()
        {
            Reserva reserva1 = new Reserva() 
            {
             CantidadButacas = 2,
             FechaAlta = DateTime.Now,
             ClienteId = BuscarCliente(44211766),
             FuncionId = BuscarFuncion("Funcion unica e inigualable"),
            };
            _context.Reservas.Add(reserva1);
            _context.SaveChanges();
        }

        private int BuscarFuncion(string descripcion)
        {
            int funcionId = 1;
            Funcion funcion = _context.Funciones.FirstOrDefault(f => f.Descripcion == descripcion);
            if (funcion != null)
            {
                funcionId = funcion.Id;
            }
            return funcionId;
        }

        private int BuscarCliente(int dni)
        {
            int clienteId = 1;
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.DNI == dni);
            if (cliente != null)
            {
                clienteId = cliente.Id;
            }
            return clienteId;
        }
    }
}
