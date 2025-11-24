using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReservarClaseModel : PageModel
    {
        private readonly IClasePresentacion _claseService;
        private readonly IReservaClasePresentacion _reservaService;

        public ReservarClaseModel(IClasePresentacion claseService, IReservaClasePresentacion reservaService)
        {
            _claseService = claseService;
            _reservaService = reservaService;
        }

        // Propiedades para la vista
        public List<Clase> Clases { get; set; } = new();
        [BindProperty] public int ClaseId { get; set; }
        [BindProperty] public DateTime Fecha { get; set; } = DateTime.Today;

        public async Task OnGet(DateTime? fecha)
        {
            Fecha = fecha ?? DateTime.Today;

            // Listar clases disponibles para la fecha
            Clases = await _claseService.ListarPorFecha(Fecha);
        }

        public async Task<IActionResult> OnPostReservar()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToPage("Login");

            try
            {
                var reserva = await _reservaService.Reservar(usuarioId.Value, ClaseId, Fecha);

                if (reserva != null)
                {
                    ViewData["Mensaje"] = "Clase reservada con éxito 🎉";
                    return RedirectToPage("ReservarClase", new { fecha = Fecha });
                }
                else
                {
                    ViewData["Mensaje"] = "No hay cupos disponibles ❌";
                    return RedirectToPage("ReservarClase", new { fecha = Fecha });
                }
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = $"Error al reservar: {ex.Message}";
                return RedirectToPage("ReservarClase", new { fecha = Fecha });
            }
        }
    }
}