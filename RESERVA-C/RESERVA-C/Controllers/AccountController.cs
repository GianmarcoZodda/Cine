using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Models;
using RESERVA_C.Models.ViewModels;

namespace RESERVA_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;

        public AccountController(UserManager<Persona> userManager)
        {
            this._userManager = userManager;
        }

        //me da el form
        public IActionResult Registrar()
        {
            return View();
        }


        //carga los dats ingresados x el usuario
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarVM model)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente()
                {
                    Nombre = model.Nombre,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var resultado = await _userManager.CreateAsync(cliente, model.Password);

                if (resultado.Succeeded)
                { 
                    return RedirectToAction("Edit", "Clientes", new { id = cliente.Id });
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View();
        }
    }
}
