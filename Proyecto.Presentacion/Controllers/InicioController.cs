using FRONT_web_personal_saving.Models;
using FRONT_web_personal_saving.Recursos;
using FRONT_web_personal_saving.Servicios.Contrato;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//referencias para los servicios
using FRONT_web_personal_saving.Models;
using FRONT_web_personal_saving.Recursos;
using FRONT_web_personal_saving.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace FRONT_web_personal_saving.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        //---------
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.contraseña = Utilidades.EncriptarContra(modelo.contraseña);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.id_usuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No fue posible registrar al usuario. Inténtelo de nuevo.";
            return View();
        }

        //LOGIN
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string email, string contraseña)
        {

            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(email, Utilidades.EncriptarContra(contraseña));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "El usuario no existe, regístrate.";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.nombre)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //popiedades de autenticacion
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
