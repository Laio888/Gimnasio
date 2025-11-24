using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class UsuarioPrincipalModel : PageModel
    {
        public string NombreUsuario { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            // Si no hay sesión → redirigir al login
            var nombre = HttpContext.Session.GetString("Nombre");

            if (string.IsNullOrEmpty(nombre))
                return RedirectToPage("/Ventanas/Login");

            NombreUsuario = nombre;
            return Page();
        }

        // Handler para cerrar sesión desde el dropdown
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Ventanas/Login");
        }
    }
}
