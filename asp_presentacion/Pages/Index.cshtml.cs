using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUsuarioPresentacion _usuarioService;

        public IndexModel(ILogger<IndexModel> logger, IUsuarioPresentacion usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        // Propiedad opcional para mostrar cantidad de usuarios registrados en el sistema
        public int TotalUsuarios { get; set; }

        public async Task OnGet()
        {
            try
            {
                // Ejemplo: obtener lista de usuarios para mostrar estadísticas en el index
                var lista = await _usuarioService.Listar();
                TotalUsuarios = lista?.Count ?? 0;

                _logger.LogInformation($"[INDEX] Usuarios registrados: {TotalUsuarios}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[INDEX] Error al cargar usuarios");
                ViewData["Mensaje"] = "Error al cargar información inicial.";
                ViewData["TipoMensaje"] = "error";
            }
        }
    }
}