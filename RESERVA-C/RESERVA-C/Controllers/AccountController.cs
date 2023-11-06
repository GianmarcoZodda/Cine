using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Models;
using RESERVA_C.Models.ViewModels;

namespace RESERVA_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _roleManager;

        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager,
            RoleManager<Rol> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
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
                    var result = await _userManager.AddToRoleAsync(cliente, "ClienteRol");
                   
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Edit", "Clientes", new { id = cliente.Id });
                    }
                 //si no se logra añadir el rol no nos va a llevar a la vista de edit. Aca abajo podemos decir que hacer en este caso 
                
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View(model);
        }



        //inicio de sesión (get)
        public IActionResult IniciarSesion(string returnurl)
        {
            TempData["ReturnUrl "] = returnurl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login modelo)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.Recordarme, false);
                if (resultado.Succeeded)
                {
                    string returnUrl = TempData["ReturnUrl"] as string;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido");
            }

            return View(modelo);
        }

        [Authorize]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("IniciarSesion", "Account");
        }

        public IActionResult AccesoDenegado()
        {
            return RedirectToAction("Index", "Home", new { mensaje = "Acceso Denegado" });
        }

    }
}
