using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Shared
{
    public class MensajesModel : PageModel
    {
        // Mensaje principal
        [BindProperty(SupportsGet = true)]
        public string Mensaje { get; set; } = string.Empty;

        // Tipo de mensaje: success, error, warning, info
        [BindProperty(SupportsGet = true)]
        public string TipoMensaje { get; set; } = "info";

        public void OnGet()
        {
            // Si no hay mensaje, se puede setear uno por defecto
            if (string.IsNullOrEmpty(Mensaje))
            {
                Mensaje = "No hay mensajes para mostrar.";
                TipoMensaje = "info";
            }
        }
    }
}