using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asp_presentacion.Pages.Shared;

namespace asp_presentacion.Pages.Ventanas
{
    public class LoginModel : PageModel
    {
        private readonly IUsuarioPresentacion iPresentacion;

        public LoginModel(IUsuarioPresentacion iPresentacion)
        {
            this.iPresentacion = iPresentacion;
        }

        [BindProperty] public string Correo { get; set; } = string.Empty;
        [BindProperty] public string PasswordHash { get; set; } = string.Empty;

        public void OnGet()
        {
            // Página de login inicial
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Console.WriteLine("====== FRONT PAGE MODEL ======");
                Console.WriteLine($"Correo en PageModel: {Correo}");
                Console.WriteLine($"PasswordHash en PageModel: {PasswordHash}");
                Console.WriteLine("================================");

                Console.WriteLine($"[LOGIN] Handler ejecutado Correo={Correo}");

                if (string.IsNullOrWhiteSpace(Correo?.Trim()) || string.IsNullOrWhiteSpace(PasswordHash?.Trim()))
                {
                    ViewData["Mensaje"] = "Debe ingresar correo y contraseña.";
                    ViewData["TipoMensaje"] = "warning";
                    return Page();
                }

                var usuario = await iPresentacion.Login(Correo, PasswordHash);
                Console.WriteLine($"[LOGIN] Servicio retornó: {(usuario != null ? usuario.Correo : "null")}");

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Correo);
                    HttpContext.Session.SetString("Nombre", usuario.Nombre ?? "");
                    HttpContext.Session.SetString("RolId", usuario.RolId.ToString());

                    Console.WriteLine($"[LOGIN OK] Sesión guardada Usuario={usuario.Correo}");

                    ViewData.Clear();

                    return RedirectToPage("/Ventanas/UsuarioPrincipal");
                }
                else
                {
                    ViewData["Mensaje"] = "Credenciales inválidas.";
                    ViewData["TipoMensaje"] = "error";
                    Console.WriteLine("[LOGIN] Credenciales inválidas");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOGIN ERROR] {ex.Message}");

                if (ex.Message == "lbFaltaInformacion")
                {
                    ViewData["Mensaje"] = "Falta información: ingrese todos los campos.";
                    ViewData["TipoMensaje"] = "warning";
                }
                else
                {
                    ViewData["Mensaje"] = $"Error en login: {ex.Message}";
                    ViewData["TipoMensaje"] = "error";
                }

                return Page();
            }
        }
    }
}