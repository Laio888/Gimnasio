using lib_dominio.Entidades;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ConfigUsuarioModel : PageModel
    {

        {
            {

            {
            }

            return Page();
        }

        public IActionResult OnPostModificar()
        {
        }

        {
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

            HttpContext.Session.SetString("Usuario", usuario.Correo);
            HttpContext.Session.SetString("Nombre", usuario.Nombre);

            ViewData["Mensaje"] = "Perfil actualizado correctamente.";
            ViewData["TipoMensaje"] = "success";

            return RedirectToPage("/Ventanas/ConfigUsuario");
    }

    {
    }
}