using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asp_presentacion.Pages.Shared;

namespace asp_presentacion.Pages.Ventanas
{
    public class UsuarioPrincipalModel : PageModel
    {
        public string? UsuarioCorreo { get; set; }

        public IActionResult OnGet()
        {
            // Verificar si hay sesión activa
            UsuarioCorreo = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(UsuarioCorreo))
            {
                // Si no hay sesión, redirigir al Login
                return RedirectToPage("/Ventanas/Login");
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Cerrar sesión
            HttpContext.Session.Clear();

            // Mensaje de feedback
            TempData["Mensaje"] = "Sesión cerrada correctamente";
            TempData["TipoMensaje"] = "info";

            // Redirigir al Login
            return RedirectToPage("/Ventanas/Login");
        }
    }
}