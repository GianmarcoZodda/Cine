using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RESERVA_C.Data;
using RESERVA_C.Models;
using RESERVA_C.Models.ViewModels;

namespace RESERVA_C.Controllers
{

    public class FuncionesController : Controller
    {
        private readonly ReservaContext _context;

        public FuncionesController(ReservaContext context)
        {
            _context = context;
        }

        //private void Ejemplo()
        //{
        //    var cant = _context.Funciones.Select(f => f.Reservas.Sum(r => r.CantidadButacas)).Where();
        //    //buscar costo por tipo de sala

        //}
        private async Task<decimal> ObtenerRecaudacion(int idPelicula)
        {
            int mesActual = DateTime.Now.Month;
            decimal total = 0;

            var funcionesXPelicula = await _context.Funciones
                .Include(f => f.Reservas)
                .Include(f => f.Sala)
                .ThenInclude(s => s.TipoSala)
                .Where(f => f.PeliculaId == idPelicula && f.FechaHora.Month == mesActual)
                .ToListAsync();

            foreach (var f in funcionesXPelicula)
            {
                int cantAsientos = f.Reservas.Sum(r => r.CantidadButacas);
                decimal precio = f.Sala.TipoSala.Precio;
                total += precio * cantAsientos;
            }

            return total;
        }

        public async Task<IActionResult> RecaudacionPorPeliculaEnMes()
        {
            // Obtener las recaudaciones por película para el mes actual
            var recaudacionesPorPelicula = new List<RecaudacionPorPeliculaVM>();

            // Asegúrate de esperar la ejecución asíncrona aquí
            var peliculas = await _context.Peliculas.ToListAsync();

            foreach (var pelicula in peliculas)
            {
                RecaudacionPorPeliculaVM recaudacionPorPelicula = new RecaudacionPorPeliculaVM()
                {
                    Id = pelicula.Id,
                    Imagen = pelicula.Imagen,
                    Titulo = pelicula.Titulo,
                    Recaudacion = await ObtenerRecaudacion(pelicula.Id)
                };
                recaudacionesPorPelicula.Add(recaudacionPorPelicula);
            }

            return View(recaudacionesPorPelicula);
        }
        // GET: Funciones
        public async Task<IActionResult> Index(int? peliculaId, int? cantidadButacas)
        {

            //Ejemplo();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaLimite = fechaActual.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddDays(7);

            List<FuncionIndexVM> funcionesAMostrar = new List<FuncionIndexVM>();
            IQueryable<Funcion> funcionesAProcesar = _context.Funciones
                   .Include(f => f.Pelicula)
                   .Include(f => f.Sala)
                   .Include(f => f.Reservas.Where(r => r.Activa));
            if (User.IsInRole("ClienteRol")) {
                funcionesAProcesar = funcionesAProcesar.Where(f => f.FechaHora >= fechaActual && f.FechaHora <= fechaLimite && f.Confirmada);
            }
            if (peliculaId.HasValue)
            {
                funcionesAProcesar = funcionesAProcesar.Where(f => f.Pelicula.Id == peliculaId);
            }
            List<FuncionIndexVM> funcionesIndexVM = CalcularButacasDisponibles(funcionesAProcesar.ToList());

            foreach (var actual in funcionesIndexVM)
            {
                if (actual.ButacasDisponibles > 0)
                {
                    if (cantidadButacas.HasValue) 
                    { if(actual.ButacasDisponibles >= cantidadButacas)
                        {
                            funcionesAMostrar.Add(actual);
                        }
                        
                    }else 
                    { funcionesAMostrar.Add(actual); }
                    
                }
            }

            if (funcionesAMostrar.Any())
            {
                return View(funcionesAMostrar);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { mensaje = "no quedan funciones disponibles" });
            }

        }

        private List<FuncionIndexVM> CalcularButacasDisponibles(List<Funcion> funciones)
        {
            List<FuncionIndexVM> lista = new List<FuncionIndexVM>();

            foreach (var funcion in funciones)
            {
                FuncionIndexVM funcionIndexVM = new FuncionIndexVM()
                {
                    Id = funcion.Id,
                    FechaHora = funcion.FechaHora,
                    Descripcion = funcion.Descripcion,
                    Confirmada = funcion.Confirmada,
                    Pelicula = funcion.Pelicula,
                    PeliculaId = funcion.PeliculaId,
                    Reservas = funcion.Reservas,
                    Sala = funcion.Sala,
                    SalaId = funcion.SalaId
                };
                if (funcionIndexVM.Sala != null && funcionIndexVM.Reservas != null)
                {
                    funcionIndexVM.ButacasDisponibles = CalcularButacas(funcion.Sala.CapacidadButacas, funcion.Reservas);
                }
                else
                {
                    funcionIndexVM.ButacasDisponibles = 0;
                }
                lista.Add(funcionIndexVM);
            }
            return lista;
        }

        private int CalcularButacas(int capacidadButacas, List<Reserva> reservas)
        {
            return capacidadButacas - GetCantidadButacas(reservas);
        }

        private int GetCantidadButacas(List<Reserva> reservas)
        {
            int resultado = 0;
            foreach (Reserva reserva in reservas)
            {
                resultado += reserva.CantidadButacas;
            }
            return resultado;
        }


        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funciones/Create
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo");
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero");
            return View();
        }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Create([Bind("Id,FechaHora,Descripcion,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
            return View(funcion);
        }

        // GET: Funciones/Edit/5
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
            return View(funcion);
        }

        // POST: Funciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHora,Descripcion,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalFuncion = await _context.Funciones.FirstOrDefaultAsync(f => f.Id == id);
                    if (originalFuncion == null)
                    {
                        return NotFound();
                    }
                    originalFuncion.FechaHora = funcion.FechaHora;
                    originalFuncion.Descripcion = funcion.Descripcion;
                    originalFuncion.Confirmada = funcion.Confirmada;
                    originalFuncion.PeliculaId = funcion.PeliculaId;
                    originalFuncion.SalaId = funcion.SalaId;
                    _context.Update(originalFuncion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Descripcion", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Id", funcion.SalaId);
            return View(funcion);
        }

        // GET: Funciones/Delete/5
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Reservas)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (funcion == null)
            {
                return NotFound();
            }

            bool hayReservasActivas = funcion.Reservas.Any(r => r.Activa);

            if (hayReservasActivas)
            {
                return RedirectToAction("Index", "Home", new { mensaje = "no se puede eliminar la funcion porque tiene reservas activas" });
            }
  
            return View(funcion);
        }

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funciones == null)
            {
                return Problem("Entity set 'ReservaContext.Funciones'  is null.");
            }
            var funcion = await _context.Funciones.FindAsync(id);

            if (funcion != null)
            {
                _context.Funciones.Remove(funcion);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { mensaje = "no existe la funcion" });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return _context.Funciones.Any(e => e.Id == id);
        }
    }
}
