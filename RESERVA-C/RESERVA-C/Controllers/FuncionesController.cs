using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: Funciones
        public async Task<IActionResult> Index(int? id)
        {
            List<FuncionIndexVM> funcionesAMostrar = new List<FuncionIndexVM>();
            //var reservaContext = _context.Funciones.Include(f => f.Pelicula).Include(f => f.Sala);
            IQueryable<Funcion> funcion = _context.Funciones
                   .Include(f => f.Pelicula)
                   .Include(f => f.Sala)
                   .Include(f => f.Reservas);
            if (id.HasValue)
            {
                funcion = funcion.Where(f => f.Pelicula.Id == id);
            }
            List<FuncionIndexVM> funcionesIndexVM = CalcularButacasDisponibles(funcion.ToList());

            foreach (var actual in funcionesIndexVM)
            {
                if (actual.ButacasDisponibles > 0 && actual.Confirmada)
                {
                    funcionesAMostrar.Add(actual);
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

        //public IActionResult Listar(int idPelicula)
        //{
        //    List<FuncionIndexVM> ListaFuncionesPorPeli = new List<FuncionIndexVM>();
        //    ListaFuncionesPorPeli = this.ObtenerFuncionesPorId(idPelicula);

        //    if (ListaFuncionesPorPeli.Count() == 0)
        //    {
        //        return RedirectToAction("Index", "Home", new { mensaje = "no quedan funciones disponibles" });
        //    }

        //    return View(ListaFuncionesPorPeli);
        //}

        //private List<FuncionIndexVM> ObtenerFuncionesPorId(int idPelicula)
        //{
        //    //recorre la lista de funciones en bd y agrega a una nueva lista las funciones q tengan = id, tengan + de 0 butacas disponibles y esten confirmadas
        //    List<FuncionIndexVM> ListaFuncionesPorPeli = new List<FuncionIndexVM>();
        //    foreach (var funcion in _context.Funciones)
        //    {
        //        if (funcion.PeliculaId == idPelicula && funcion.ButacasDisponibles > 0 && funcion.Confirmada)
        //        {
        //            FuncionIndexVM funcionIndexVM = new FuncionIndexVM()
        //            {
        //                Id = funcion.Id,
        //                FechaHora = funcion.FechaHora,
        //                Descripcion = funcion.Descripcion,
        //                Confirmada = funcion.Confirmada,
        //                Pelicula = funcion.Pelicula,
        //                PeliculaId = funcion.PeliculaId,
        //                Reservas = funcion.Reservas,
        //                Sala = funcion.Sala,
        //                SalaId = funcion.SalaId
        //            };

        //            ListaFuncionesPorPeli.Add(funcionIndexVM);
        //        }
        //    }
        //    return ListaFuncionesPorPeli;
        //}

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
                    _context.Update(funcion);
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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return _context.Funciones.Any(e => e.Id == id);
        }
    }
}
