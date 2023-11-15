using System;
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

namespace RESERVA_C.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservaContext _context;
        private readonly UserManager<Persona> _userManager;

        public ReservasController(ReservaContext context, UserManager<Persona> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var reservaContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.Funcion);
            return View(await reservaContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create(int? funcionId)
        {
            if (User.IsInRole("AdminRol") || User.IsInRole("EmpleadoRol"))
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido");
            }
            if (funcionId.HasValue)
            {
                ViewData["FuncionId"] = new SelectList(_context.Funciones.Where(f => f.Id == funcionId).Include(f => f.Pelicula).Include(f => f.Sala), "Id", "FuncionCompleta");
            }
            else
            {
                ViewData["FuncionId"] = new SelectList(_context.Funciones.Include(f => f.Pelicula).Include(f => f.Sala), "Id", "FuncionCompleta");
            }
            // ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido");
            // ViewData["FuncionId"] = new SelectList(_context.Funciones.Include(f => f.Pelicula).Include(f => f.Sala), "Id", "FuncionCompleta");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? funcionId, [Bind("Id,FuncionId,FechaAlta,ClienteId,CantidadButacas")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("ClienteRol"))
                {
                    int clienteId = Int32.Parse(_userManager.GetUserId(User));
                    reserva.ClienteId = clienteId;
                }
                reserva.FechaAlta = DateTime.Now;
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuncionId,FechaAlta,ClienteId,CantidadButacas")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'ReservaContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
