using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class RutinaNucleo
    {
        public static Rutina Rutina(int usuarioId)
        {
            var entidad = new Rutina();
            entidad.Nombre = "Rutina-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Descripcion = "Rutina de prueba automatizada";
            entidad.Nivel = "Intermedio";
            entidad.DuracionMin = 45;
            entidad.UsuarioId = usuarioId;
            return entidad;
        }
    }
}