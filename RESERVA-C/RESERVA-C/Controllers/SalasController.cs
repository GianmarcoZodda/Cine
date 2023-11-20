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
    [Authorize(Roles = "AdminRol, EmpleadoRol")]
    public class SalasController : Controller
    {
        private readonly ReservaContext _context;

        public SalasController(ReservaContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            var reservaContext = _context.Salas.Include(s => s.TipoSala);
            return View(await reservaContext.ToListAsync());
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(s => s.TipoSala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Salas/Create
        public IActionResult Create()
        {
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSalas, "Id", "Nombre");
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,TipoSalaId,CapacidadButacas")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,TipoSalaId,CapacidadButacas")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalSala = await _context.Salas
                        .Include(s => s.Funciones)
                        .ThenInclude(f => f.Reservas)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (originalSala == null)
                    {
                        return NotFound();
                    }

                    originalSala.Numero = sala.Numero;
                    originalSala.CapacidadButacas = sala.CapacidadButacas;

                    // Verificar si hay funciones con reservas activas
                    bool hayFuncionesConReservasActivas = originalSala.Funciones.Any(f => f.Reservas.Any(r => r.Activa));

                    if (!hayFuncionesConReservasActivas)
                    {
                        originalSala.TipoSalaId = sala.TipoSalaId;
                    }

                    _context.Update(originalSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
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

            ViewData["TipoSalaId"] = new SelectList(_context.TipoSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        private bool SalaExists(int id)
        {
          return _context.Salas.Any(e => e.Id == id);
        }
    }
}
