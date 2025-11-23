using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ConfigUsuarioModel : PageModel
    {
        // Propiedad que se usa en la vista
        public UsuarioDto? Usuario { get; set; }

        public void OnGet()
        {
            // Aquí deberías cargar el usuario desde sesión o base de datos
            Usuario = new UsuarioDto
            {
                Nombre = HttpContext.Session.GetString("Nombre"),
                Correo = HttpContext.Session.GetString("Usuario"),
                RolId = Convert.ToInt32(HttpContext.Session.GetString("RolId") ?? "1")
            };

            if (string.IsNullOrEmpty(Usuario.Correo))
            {
                // Si no hay sesión activa, redirigir al Login
                Response.Redirect("/Ventanas/Login");
            }
        }

        public IActionResult OnPostModificar()
        {
            // Aquí iría la lógica para modificar perfil (ejemplo: abrir formulario)
            ViewData["Mensaje"] = "Funcionalidad de modificar perfil en construcción";
            ViewData["TipoMensaje"] = "info";
            return Page();
        }

        public IActionResult OnPostBorrar()
        {
            // Aquí iría la lógica para eliminar el usuario de la BD
            HttpContext.Session.Clear();
            ViewData["Mensaje"] = "Cuenta eliminada correctamente";
            ViewData["TipoMensaje"] = "success";
            return RedirectToPage("/Ventanas/Login");
        }
    }

    // DTO simple para la vista
    public class UsuarioDto
    {
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public int RolId { get; set; }
    }
}