using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RESERVA_C.Data;
using RESERVA_C.Models;

namespace RESERVA_C.Controllers
{
    public class TiposSalasController : Controller
    {
        private readonly ReservaContext _context;

        public TiposSalasController(ReservaContext context)
        {
            _context = context;
        }

        // GET: TiposSalas
        public async Task<IActionResult> Index()
        {
              return View(await _context.TipoSalas.ToListAsync());
        }

        // GET: TiposSalas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoSalas == null)
            {
                return NotFound();
            }

            var tipoSala = await _context.TipoSalas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSala == null)
            {
                return NotFound();
            }

            return View(tipoSala);
        }

        // GET: TiposSalas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposSalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoSala);
        }

        // GET: TiposSalas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoSalas == null)
            {
                return NotFound();
            }

            var tipoSala = await _context.TipoSalas.FindAsync(id);
            if (tipoSala == null)
            {
                return NotFound();
            }
            return View(tipoSala);
        }

        // POST: TiposSalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (id != tipoSala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoSalaExists(tipoSala.Id))
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
            return View(tipoSala);
        }

        // GET: TiposSalas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoSalas == null)
            {
                return NotFound();
            }

            var tipoSala = await _context.TipoSalas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSala == null)
            {
                return NotFound();
            }

            return View(tipoSala);
        }

        // POST: TiposSalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoSalas == null)
            {
                return Problem("Entity set 'ReservaContext.TipoSalas'  is null.");
            }
            var tipoSala = await _context.TipoSalas.FindAsync(id);
            if (tipoSala != null)
            {
                _context.TipoSalas.Remove(tipoSala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoSalaExists(int id)
        {
          return _context.TipoSalas.Any(e => e.Id == id);
        }
    }
}
