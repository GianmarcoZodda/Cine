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
    public class PersonasController : Controller
    {
        private readonly ReservaContext _context;

        public PersonasController(ReservaContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Personas.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,DNI,Telefono,Direccion,UserName,Password,Email,FechaAlta")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,DNI,Telefono,Direccion,UserName,Password,Email,FechaAlta")] Persona updatedPersona)
        {
            if (id != updatedPersona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // @MARIANO: Esto lo chatgpteamos, nos estaba modificando el fechadealta al actualizar todo el objeto con update y nos ponia por defecto 01/01/01.
                    // Aca encontramos una solucion que es almacenar el objeto persona, hacer una variable updatedPersona que almacene la persona y excluir el campo fechaAlta.
                    // Cual seria la manera de resolverlo?
                    var originalPersona = await _context.Personas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

                    if (originalPersona == null)
                    {
                        return NotFound();
                    }                    
                    // Copy the FechaAlta value from the original Persona to the updated one
                    updatedPersona.FechaAlta = originalPersona.FechaAlta;
                    // Exclude FechaAlta from the update
                    _context.Entry(originalPersona).State = EntityState.Detached; // Detach the original object
                    _context.Entry(updatedPersona).State = EntityState.Modified; // Mark the updated object as modified

                    _context.Update(updatedPersona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(updatedPersona.Id))
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
            return View(updatedPersona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'ReservaContext.Personas'  is null.");
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return _context.Personas.Any(e => e.Id == id);
        }
    }
}
