using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ConfigUsuarioModel : PageModel
    {
        private readonly IUsuarioPresentacion _usuarioService;

        public ConfigUsuarioModel(IUsuarioPresentacion usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario? Usuario { get; set; }

        [BindProperty] public string Nombre { get; set; } = string.Empty;
        [BindProperty] public string Correo { get; set; } = string.Empty;
        [BindProperty] public string PasswordActual { get; set; } = string.Empty;
        [BindProperty] public string PasswordNueva { get; set; } = string.Empty;

        public async Task<IActionResult> OnGet()
        {
            var correoSesion = HttpContext.Session.GetString("Usuario");
            if (correoSesion == null)
                return RedirectToPage("/Ventanas/Login");

            // Buscar usuario por correo
            var entidad = new Usuario { Correo = correoSesion };
            var lista = await _usuarioService.PorNombre(entidad);
            Usuario = lista.FirstOrDefault();

            // Precargar datos para mostrar/editar
            if (Usuario != null)
            {
                Nombre = Usuario.Nombre ?? "";
                Correo = Usuario.Correo ?? "";
            }

            return Page();
        }

        // Abrir ventana de modificar (si la tienes aparte)
        public IActionResult OnPostModificar()
        {
            return RedirectToPage("/Ventanas/ModificarUsuario");
        }

        public async Task<IActionResult> OnPostGuardar()
        {
            var correoSesion = HttpContext.Session.GetString("Usuario");
            if (correoSesion == null)
                return RedirectToPage("/Ventanas/Login");

            var entidad = new Usuario { Correo = correoSesion };
            var lista = await _usuarioService.PorNombre(entidad);
            var usuario = lista.FirstOrDefault();

            if (usuario == null)
                return RedirectToPage("/Ventanas/Login");

            // Validación de cambio de contraseña
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

            // Actualiza nombre y correo
            usuario.Nombre = Nombre;
            usuario.Correo = Correo;

            // Guardar cambios
            await _usuarioService.Modificar(usuario);

            // Actualizar sesión
            HttpContext.Session.SetString("Usuario", usuario.Correo);
            HttpContext.Session.SetString("Nombre", usuario.Nombre);

            ViewData["Mensaje"] = "Perfil actualizado correctamente.";
            ViewData["TipoMensaje"] = "success";

            return RedirectToPage("/Ventanas/ConfigUsuario");
        }

        // CONFIRMADO PARA BORRAR
        public async Task<IActionResult> OnPostBorrar()
        {
            var correoSesion = HttpContext.Session.GetString("Usuario");
            if (correoSesion == null)
                return RedirectToPage("/Ventanas/Login");

            var entidad = new Usuario { Correo = correoSesion };
            var lista = await _usuarioService.PorNombre(entidad);
            var usuario = lista.FirstOrDefault();

            if (usuario != null)
                await _usuarioService.Borrar(usuario);

            HttpContext.Session.Clear();

            return RedirectToPage("/Ventanas/Login");
        }
    }
}
