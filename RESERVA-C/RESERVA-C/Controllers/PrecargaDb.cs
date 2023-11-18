using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Data;
using RESERVA_C.Helpers;
using RESERVA_C.Models;
using System.Collections.Generic;

namespace RESERVA_C.Controllers
{
    public class PrecargaDb : Controller
    {

        private readonly ReservaContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _roleManager;

        public PrecargaDb(ReservaContext context, UserManager<Persona> userManager, SignInManager<Persona> signInManager,
            RoleManager<Rol> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public IActionResult Seed()
        {
            CrearRoles().Wait();

            if (!_context.Personas.Any())
            {
                this.AddAdmin().Wait();
            }
            

            if (!_context.Clientes.Any())
            {
                this.AddClientes().Wait();
            }

            if (!_context.Empleados.Any())
            {
                this.AddEmpleados().Wait();
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

        private async Task CrearRoles()
        {
           Rol rolcliente = new Rol() { Name = "ClienteRol"};
           Rol rolempleado = new Rol() { Name = "EmpleadoRol" };
           Rol roladmin = new Rol() { Name = "AdminRol" };

            await _roleManager.CreateAsync(rolcliente);
            await _roleManager.CreateAsync(rolempleado);
            await _roleManager.CreateAsync(roladmin);
        }

        public IActionResult Remove()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return RedirectToAction("Index", "Home", new {mensaje = "Borre"});
        }
        private async Task AddClientes()
        {
            Cliente cliente1 = new Cliente()
            {
                Nombre = "Gianmarco",
                Apellido = "Zodda",
                DNI = 44211766,
                Telefono = 100000001,
                Direccion = "san martin",
                UserName = "cliente1@ort.edu.ar",
                Email = "cliente1@ort.edu.ar",
                FechaAlta = DateTime.Now
            };
            await _userManager.CreateAsync(cliente1, "Password1!");
            await _userManager.AddToRoleAsync(cliente1, "ClienteRol");
            Cliente cliente2 = new Cliente()
            {
                Nombre = "Carlos",
                Apellido = "Tevez",
                DNI = 33211766,
                Telefono = 100000011,
                Direccion = "Devoto",
                UserName = "cliente2@ort.edu.ar",
                Email = "cliente2@ort.edu.ar",
                FechaAlta = DateTime.Now
            };
            await _userManager.CreateAsync(cliente2, "Password1!");
            await _userManager.AddToRoleAsync(cliente2, "ClienteRol");
            Cliente cliente3 = new Cliente()
            {
                Nombre = "Eduardo",
                Apellido = "Martínez",
                DNI = 77665544,
                Telefono = 100004455,
                Direccion = "Belgrano",
                UserName = "cliente3@ort.edu.ar",
                Email = "cliente3@ort.edu.ar",
                FechaAlta = DateTime.Now
            };
            await _userManager.CreateAsync(cliente3, "Password1!");
            await _userManager.AddToRoleAsync(cliente3, "ClienteRol");
            Cliente cliente4 = new Cliente()
            {
                Nombre = "Ana",
                Apellido = "López",
                DNI = 88993322,
                Telefono = 100005566,
                Direccion = "Caballito",
                UserName = "cliente4@ort.edu.ar",
                Email = "cliente4@ort.edu.ar",
                FechaAlta = DateTime.Now
            };
            await _userManager.CreateAsync(cliente4, "Password1!");
            await _userManager.AddToRoleAsync(cliente4, "ClienteRol");
            Cliente cliente5 = new Cliente()
            {
                Nombre = "Luis",
                Apellido = "Rodríguez",
                DNI = 11223344,
                Telefono = 100006677,
                Direccion = "Villa Urquiza",
                UserName = "cliente5@ort.edu.ar",
                Email = "cliente5@ort.edu.ar",
                FechaAlta = DateTime.Now
            };
            await _userManager.CreateAsync(cliente5, "Password1!");
            await _userManager.AddToRoleAsync(cliente5, "ClienteRol");
        }
        private async Task AddEmpleados()
        {
            Empleado empleado1 = new Empleado()
            {
                Nombre = "Nahuel",
                Apellido = "David",
                DNI = 43035648,
                Telefono = 100000001,
                Direccion = "Montecastro",
                UserName = "empleado1@ort.edu.ar",
                Email = "empleado1@ort.edu.ar",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            await _userManager.CreateAsync(empleado1, "Password1!");
            await _userManager.AddToRoleAsync(empleado1, "EmpleadoRol");
            Empleado empleado2 = new Empleado()
            {
                Nombre = "Fernando",
                Apellido = "Sánchez",
                DNI = 39781234,
                Telefono = 200000002,
                Direccion = "Recoleta",
                UserName = "empleado2@ort.edu.ar",
                Email = "empleado2@ort.edu.ar",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            await _userManager.CreateAsync(empleado2, "Password1!");
            await _userManager.AddToRoleAsync(empleado2, "EmpleadoRol");

            Empleado empleado3 = new Empleado()
            {
                Nombre = "Lorena",
                Apellido = "Fernández",
                DNI = 65432198,
                Telefono = 200000003,
                Direccion = "Devoto",
                UserName = "empleado3@ort.edu.ar",
                Email = "empleado3@ort.edu.ar",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            await _userManager.CreateAsync(empleado3, "Password1!");
            await _userManager.AddToRoleAsync(empleado3, "EmpleadoRol");

            Empleado empleado4 = new Empleado()
            {
                Nombre = "Sergio",
                Apellido = "García",
                DNI = 98123456,
                Telefono = 200000004,
                Direccion = "Montecastro",
                UserName = "empleado4@ort.edu.ar",
                Email = "empleado4@ort.edu.ar",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            await _userManager.CreateAsync(empleado4, "Password1!");
            await _userManager.AddToRoleAsync(empleado4, "EmpleadoRol");

            Empleado empleado5 = new Empleado()
            {
                Nombre = "Ana",
                Apellido = "Rodríguez",
                DNI = 75648912,
                Telefono = 200000005,
                Direccion = "Villa Urquiza",
                UserName = "empleado5@ort.edu.ar",
                Email = "empleado5@ort.edu.ar",
                FechaAlta = DateTime.Now,
                Legajo = Generadores.GetNewLegajo(5)
            };
            await _userManager.CreateAsync(empleado5, "Password1!");
            await _userManager.AddToRoleAsync(empleado5, "EmpleadoRol");
        }
        private void AddGeneros() 
        {
            Genero genero1 = new Genero()
            {
                Nombre = "Ciencia Ficcion"
            };
            _context.Generos.Add(genero1);
            _context.SaveChanges();

            Genero genero2 = new Genero()
            {
                Nombre = "Misterio"
            };
            _context.Generos.Add(genero2);
            _context.SaveChanges();

            Genero genero3 = new Genero()
            {
                Nombre = "Romance"
            };
            _context.Generos.Add(genero3);
            _context.SaveChanges();

            Genero genero4 = new Genero()
            {
                Nombre = "Aventura"
            };
            _context.Generos.Add(genero4);
            _context.SaveChanges();

            Genero genero5 = new Genero()
            {
                Nombre = "Historia"
            };
            _context.Generos.Add(genero5);
            _context.SaveChanges();

        }

        private void AddPeliculas()
        {
            Pelicula pelicula = new Pelicula()
            {
                FechaLanzamiento = new DateTime(2006,04,14),
                Titulo = "Piratas del Caribe",
                Descripcion = "Aparece Jack Sparrow y se pelea con el que tiene la pata de palo",
                GeneroId = BuscarGenero("Ciencia Ficcion"),
            };
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();

            Pelicula pelicula2 = new Pelicula()
            {
                FechaLanzamiento = new DateTime(2010, 08, 25),
                Titulo = "El Enigma del Candelabro",
                Descripcion = "Un misterioso asesinato en una mansión.",
                GeneroId = BuscarGenero("Misterio")
            };
            _context.Peliculas.Add(pelicula2);
            _context.SaveChanges();

            Pelicula pelicula3 = new Pelicula()
            {
                FechaLanzamiento = new DateTime(2015, 12, 10),
                Titulo = "Amor en París",
                Descripcion = "Una historia de amor en la ciudad de la luz.",
                GeneroId = BuscarGenero("Romance")
            };
            _context.Peliculas.Add(pelicula3);
            _context.SaveChanges();

            Pelicula pelicula4 = new Pelicula()
            {
                FechaLanzamiento = new DateTime(2018, 06, 30),
                Titulo = "La Búsqueda del Tesoro Perdido",
                Descripcion = "Aventuras en busca de un tesoro oculto.",
                GeneroId = BuscarGenero("Aventura")
            };
            _context.Peliculas.Add(pelicula4);
            _context.SaveChanges();

            Pelicula pelicula5 = new Pelicula()
            {
                FechaLanzamiento = new DateTime(2022, 03, 15),
                Titulo = "La Historia Olvidada",
                Descripcion = "Una épica saga histórica.",
                GeneroId = BuscarGenero("Historia")
            };
            _context.Peliculas.Add(pelicula5);
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
            TipoSala tipoSala2 = new TipoSala()
            {
                Nombre = "IMAX",
                Precio = 60
            };

            _context.TipoSalas.Add(tipoSala2);
            _context.SaveChanges();

            TipoSala tipoSala3 = new TipoSala()
            {
                Nombre = "VIP",
                Precio = 70
            };
            _context.TipoSalas.Add(tipoSala3);
            _context.SaveChanges();

            TipoSala tipoSala4 = new TipoSala()
            {
                Nombre = "4D",
                Precio = 75
            };
            _context.TipoSalas.Add(tipoSala4);
            _context.SaveChanges();

            TipoSala tipoSala5 = new TipoSala()
            {
                Nombre = "Estándar",
                Precio = 40
            };
            _context.TipoSalas.Add(tipoSala5);
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

            Sala sala2 = new Sala()
            {
                Numero = 13,
                CapacidadButacas = 120,
                TipoSalaId = BuscarTipoSala("IMAX"),
            };
            _context.Salas.Add(sala2);
            _context.SaveChanges();

            Sala sala3 = new Sala()
            {
                Numero = 14,
                CapacidadButacas = 80,
                TipoSalaId = BuscarTipoSala("VIP"),
            };
            _context.Salas.Add(sala3);
            _context.SaveChanges();

            Sala sala4 = new Sala()
            {
                Numero = 15,
                CapacidadButacas = 150,
                TipoSalaId = BuscarTipoSala("4D"),
            };
            _context.Salas.Add(sala4);
            _context.SaveChanges();

            Sala sala5 = new Sala()
            {
                Numero = 16,
                CapacidadButacas = 90,
                TipoSalaId = BuscarTipoSala("Estándar"),
            };
            _context.Salas.Add(sala5);
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
                FechaHora = DateTime.Now.AddDays(3),
                Descripcion = "Funcion de jack sparrow",
                PeliculaId = BuscarPelicula("Piratas del Caribe"),
                SalaId = BuscarSala(12),
                Confirmada = true
            };
            _context.Funciones.Add(funcion1);
            _context.SaveChanges();

            Funcion funcion2 = new Funcion()
            {
                FechaHora = DateTime.Now.AddDays(6),
                Descripcion = "Gran aventura en alta mar",
                PeliculaId = BuscarPelicula("El Enigma del Candelabro"),
                SalaId = BuscarSala(13),
                Confirmada = true
            };
            _context.Funciones.Add(funcion2);
            _context.SaveChanges();

            Funcion funcion3 = new Funcion()
            {
                FechaHora = DateTime.Now.AddDays(2),
                Descripcion = "Romance en París",
                PeliculaId = BuscarPelicula("Amor en París"),
                SalaId = BuscarSala(14),
                Confirmada = true
            };
            _context.Funciones.Add(funcion3);
            _context.SaveChanges();

            Funcion funcion4 = new Funcion()
            {
                FechaHora = DateTime.Now.AddDays(1),
                Descripcion = "La búsqueda del tesoro perdido",
                PeliculaId = BuscarPelicula("La Búsqueda del Tesoro Perdido"),
                SalaId = BuscarSala(15),
                Confirmada = true
            };
            _context.Funciones.Add(funcion4);
            _context.SaveChanges();

            Funcion funcion5 = new Funcion()
            {
                FechaHora = DateTime.Now.AddDays(4),
                Descripcion = "Épica historia del pasado",
                PeliculaId = BuscarPelicula("La Historia Olvidada"),
                SalaId = BuscarSala(16),
                Confirmada = true
            };
            _context.Funciones.Add(funcion5);
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

            Reserva reserva2 = new Reserva()
            {
                CantidadButacas = 3,
                FechaAlta = DateTime.Now,
                ClienteId = BuscarCliente(33211766),
                FuncionId = BuscarFuncion("Gran aventura en alta mar"),
            };
            _context.Reservas.Add(reserva2);
            _context.SaveChanges();

            Reserva reserva3 = new Reserva()
            {
                CantidadButacas = 4,
                FechaAlta = DateTime.Now,
                ClienteId = BuscarCliente(77665544),
                FuncionId = BuscarFuncion("Romance en París"),
            };
            _context.Reservas.Add(reserva3);
            _context.SaveChanges();

            Reserva reserva4 = new Reserva()
            {
                CantidadButacas = 2,
                FechaAlta = DateTime.Now,
                ClienteId = BuscarCliente(88993322),
                FuncionId = BuscarFuncion("La búsqueda del tesoro perdido"),
            };
            _context.Reservas.Add(reserva4);
            _context.SaveChanges();

            Reserva reserva5 = new Reserva()
            {
                CantidadButacas = 5,
                FechaAlta = DateTime.Now,
                ClienteId = BuscarCliente(11223344),
                FuncionId = BuscarFuncion("Épica historia del pasado"),
            };
            _context.Reservas.Add(reserva5);
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
        private async Task AddAdmin()
        {
            Persona admin = new Persona()
            {
                Nombre = "Mariano",
                Apellido = "Longo",
                DNI = 11111111,
                Telefono = 000000001,
                Direccion = "Belgrano",
                UserName = "admin@ort.edu.ar",
                Email = "admin@ort.edu.ar",
                FechaAlta = DateTime.Now,
            };
            await _userManager.CreateAsync(admin, "Password1!");
            await _userManager.AddToRoleAsync(admin, "AdminRol");
        }
    }
}
