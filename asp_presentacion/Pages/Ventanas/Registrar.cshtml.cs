using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                // Validación básica
                if (string.IsNullOrWhiteSpace(Nombre) ||
                    string.IsNullOrWhiteSpace(Correo) ||
                    string.IsNullOrWhiteSpace(PasswordHash))
                {
                    ViewData["Mensaje"] = "Debe ingresar todos los campos.";
                    ViewData["TipoMensaje"] = "warning";
                    return Page();
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre = Nombre,
                    Correo = Correo,
                    PasswordHash = PasswordHash, // Plano en este proyecto
                    RolId = 1
                };

                var usuario = await iPresentacion.Registrar(nuevoUsuario);

                if (usuario != null)
                {
                    // Guardar sesión
                    HttpContext.Session.SetString("Usuario", usuario.Correo);
                    HttpContext.Session.SetString("Nombre", usuario.Nombre ?? "");
                    HttpContext.Session.SetString("RolId", usuario.RolId.ToString());

                    ViewData.Clear();

                    // 🔥 ESTA ERA LA PARTE QUE FALTABA 🔥
                    return RedirectToPage("/Ventanas/UsuarioPrincipal");
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
