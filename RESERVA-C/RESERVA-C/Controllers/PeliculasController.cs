using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RESERVA_C.Data;
using RESERVA_C.Models;

namespace RESERVA_C.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly ReservaContext _context;

        public PeliculasController(ReservaContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(int? generoId)
        {
            var generos = _context.Generos.ToList();

            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");

            var peliculas = _context.Peliculas.ToList();

            if (generoId.HasValue)
            {
                peliculas = peliculas.Where(p => p.GeneroId == generoId.Value).ToList();
            }

            return View(peliculas);
        }
        public IActionResult Reservar(int peliculaId)
        {
            ViewBag.PeliculaId = peliculaId;
            return View();
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Create([Bind("Id,FechaLanzamiento,Titulo,Descripcion,Imagen,GeneroId")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaLanzamiento,Titulo,Descripcion,Imagen,GeneroId")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'ReservaContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
