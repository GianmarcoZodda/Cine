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
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ReservaContext _context;
        private readonly UserManager<Persona> _userManager;

        public ClientesController(ReservaContext context, UserManager<Persona> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Clientes
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Index()
        {
            var usuario = User.Claims.FirstOrDefault();
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize(Roles = "AdminRol, EmpleadoRol, ClienteRol")]
        public async Task<IActionResult> Details(int? id)
        {
            int personaId = Int32.Parse(_userManager.GetUserId(User));

            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            // Aca chequeamos que el usuario este entrando al Details de su propio perfil y no al de un tercero.
            if (personaId != id && User.IsInRole("ClienteRol"))
            {
                return RedirectToAction("Index", "Home", new { mensaje = "Acceso Denegado" });
            }
            else
            {
                return RedirectToAction("Edit", "Cliente", new { id = personaId });
            }


        }

        // GET: Clientes/Create
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,DNI,Telefono,Direccion,Password,Email")] Cliente cliente)
        {
            cliente.UserName = cliente.Email;
            if (ModelState.IsValid)
            {
                cliente.FechaAlta = DateTime.Now;
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5       
        [Authorize(Roles = "AdminRol, EmpleadoRol, ClienteRol")]
        public async Task<IActionResult> Edit(int? id)
        {
            int usuarioId = Int32.Parse(_userManager.GetUserId(User));

            if (id == null || _context.Clientes == null)
            {
                id = usuarioId;
            }

            if (User.IsInRole("ClienteRol"))
            {
                if (id != null && usuarioId != id)
                {
                    //atrevido
                    return RedirectToAction("Edit", "Clientes", new { id = usuarioId });
                }

            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            string username = User.Identity.Name;

            if (username.ToUpper().Equals(cliente.NormalizedEmail))
            {
                return View(cliente);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol, ClienteRol")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Telefono,Direccion")] Cliente updatedCliente)
        {
            {
                if (id != updatedCliente.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var originalCliente = await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
                        if (originalCliente == null)
                        {
                            return NotFound();
                        }
                        originalCliente.Nombre = originalCliente.Nombre;
                        originalCliente.Apellido = originalCliente.Apellido;
                        originalCliente.DNI = originalCliente.DNI;
                        originalCliente.Telefono = updatedCliente.Telefono;
                        originalCliente.Direccion = updatedCliente.Direccion;
                        originalCliente.UserName = originalCliente.Email;
                        originalCliente.Password = originalCliente.Password;
                        originalCliente.Email = originalCliente.Email;
                        _context.Update(originalCliente);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(updatedCliente.Id))
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
                return View(updatedCliente);
            }
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ReservaContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "AdminRol, EmpleadoRol")]
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }



        //registro

        [Authorize(Roles = "AdminRol, EmpleadoRol, ClienteRol")]
        public async Task<IActionResult> EditRegistro(int? id)
        {
            int usuarioId = Int32.Parse(_userManager.GetUserId(User));

            if (id == null || _context.Clientes == null)
            {
                id = usuarioId;
            }

            if (User.IsInRole("ClienteRol"))
            {
                if (id != null && usuarioId != id)
                {
                    //atrevido
                    return RedirectToAction("Edit", "Clientes", new { id = usuarioId });
                }

            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            string username = User.Identity.Name;

            if (username.ToUpper().Equals(cliente.NormalizedEmail))
            {
                return View(cliente);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRol, EmpleadoRol, ClienteRol")]
        public async Task<IActionResult> EditRegistro(int id, [Bind("Id,Apellido,DNI,Telefono,Direccion")] Cliente updatedCliente)
        {
            {
                if (id != updatedCliente.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var originalCliente = await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
                        if (originalCliente == null)
                        {
                            return NotFound();
                        }
                        originalCliente.Nombre = originalCliente.Nombre;
                        originalCliente.Apellido = updatedCliente.Apellido;
                        originalCliente.DNI = updatedCliente.DNI;
                        originalCliente.Telefono = updatedCliente.Telefono;
                        originalCliente.Direccion = updatedCliente.Direccion;
                        originalCliente.Email = originalCliente.Email;
                        originalCliente.UserName = originalCliente.Email;
                        originalCliente.Password = originalCliente.Password;
                        _context.Update(originalCliente);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(updatedCliente.Id))
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
                return View(updatedCliente);
            }
        }


    }
}
