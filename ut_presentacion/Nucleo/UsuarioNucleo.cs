using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class UsuarioNucleo
    {
        public static Usuario Usuario()
        {
            var entidad = new Usuario();
            entidad.Nombre = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Correo = "pruebas" + DateTime.Now.ToString("yyyyMMddhhmmss") + "@gym.com";
            entidad.PasswordHash = "hashPruebas";
            entidad.FechaRegistro = DateTime.Now;
            entidad.RolId = 1; // Cliente por defecto
            return entidad;
        }
    }
}