using lib_presentacion.Implementaciones;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                IUsuarioPresentacion iPresentacion
                    = new UsuarioPresentacion();
                var tarea = iPresentacion.Listar();
                tarea.Wait();
                var respuesta = tarea.Result;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
