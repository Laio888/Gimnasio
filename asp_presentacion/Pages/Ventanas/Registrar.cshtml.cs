using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asp_presentacion.Pages.Shared;

namespace asp_presentacion.Pages.Ventanas
{
    public class RegistrarModel : PageModel
    {
        private readonly IUsuarioPresentacion iPresentacion;

        public RegistrarModel(IUsuarioPresentacion iPresentacion)
        {
            this.iPresentacion = iPresentacion;
        }

        [BindProperty] public string Nombre { get; set; } = string.Empty;
        [BindProperty] public string Correo { get; set; } = string.Empty;
        [BindProperty] public string PasswordHash { get; set; } = string.Empty;

        public void OnGet()
        {
            // Página de registro inicial
        }

        public async Task<IActionResult> OnPostRegistrar()
        {
            try
            {
                // Validación básica antes de registrar
                if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(PasswordHash))
                {
                    ViewData["Mensaje"] = "Debe ingresar todos los campos.";
                    ViewData["TipoMensaje"] = "warning";
                    return Page();
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre = Nombre,
                    Correo = Correo,
                    PasswordHash = PasswordHash, // ⚠️ Texto plano en este proyecto académico
                    RolId = 1 // Usuario por defecto
                };

                var usuario = await iPresentacion.Registrar(nuevoUsuario);

                if (usuario != null)
                {
                    // Iniciar sesión automáticamente tras registrar
                    HttpContext.Session.SetString("Usuario", usuario.Correo);
                    HttpContext.Session.SetString("Nombre", usuario.Nombre ?? "");
                    HttpContext.Session.SetString("RolId", usuario.RolId.ToString());

                    // 🔑 Limpia cualquier mensaje previo
                    ViewData.Clear();

                    // Redirige al portal principal del usuario (dashboard)
                }
                else
                {
                    ViewData["Mensaje"] = "Error al registrar usuario.";
                    ViewData["TipoMensaje"] = "error";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "lbFaltaInformacion")
                {
                    ViewData["Mensaje"] = "Falta información: ingrese todos los campos.";
                    ViewData["TipoMensaje"] = "warning";
                }
                else
                {
                    ViewData["Mensaje"] = $"Error en registro: {ex.Message}";
                    ViewData["TipoMensaje"] = "error";
                }

                return Page();
            }
        }
    }
}