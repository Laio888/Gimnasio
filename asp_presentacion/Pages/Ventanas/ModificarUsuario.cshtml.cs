using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ModificarUsuarioModel : PageModel
    {
        private readonly IUsuarioPresentacion _usuarioService;

        public ModificarUsuarioModel(IUsuarioPresentacion usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario? UsuarioActual { get; set; }

        [BindProperty] public string Nombre { get; set; } = string.Empty;
        [BindProperty] public string Correo { get; set; } = string.Empty;

        [BindProperty] public string PasswordActual { get; set; } = string.Empty;
        [BindProperty] public string PasswordNueva { get; set; } = string.Empty;

        public async Task<IActionResult> OnGet()
        {
            var correoSesion = HttpContext.Session.GetString("Usuario");
            if (correoSesion == null)
                return RedirectToPage("/Ventanas/Login");

            var entidad = new Usuario { Correo = correoSesion };
            var lista = await _usuarioService.PorNombre(entidad);
            UsuarioActual = lista.FirstOrDefault();

            if (UsuarioActual == null)
                return RedirectToPage("/Ventanas/Login");

            // Rellenar campos
            Nombre = UsuarioActual.Nombre;
            Correo = UsuarioActual.Correo;

            return Page();
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            var correoSesion = HttpContext.Session.GetString("Usuario");
            if (correoSesion == null) return RedirectToPage("/Ventanas/Login");

            var entidad = new Usuario { Correo = correoSesion };
            var lista = await _usuarioService.PorNombre(entidad);
            var usuario = lista.FirstOrDefault();

            if (usuario == null)
                return RedirectToPage("/Ventanas/Login");

            if (!string.IsNullOrEmpty(PasswordNueva))
            {
                if (usuario.PasswordHash != PasswordActual)
                {
                    ViewData["Mensaje"] = "La contraseña actual no es correcta.";
                    ViewData["TipoMensaje"] = "error";
                    return Page();
                }

                usuario.PasswordHash = PasswordNueva;
            }

            usuario.Nombre = Nombre;
            usuario.Correo = Correo;

            await _usuarioService.Modificar(usuario);

            HttpContext.Session.SetString("Nombre", usuario.Nombre);
            HttpContext.Session.SetString("Usuario", usuario.Correo);

            return RedirectToPage("/Ventanas/ConfigUsuario");
        }
    }
}
