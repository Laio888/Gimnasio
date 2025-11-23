using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class NotificacionNucleo
    {
        public static Notificacion Notificacion(int usuarioId)
        {
            return new Notificacion
            {
                UsuarioId = usuarioId,
                Texto = "Notificación de prueba",
                Tipo = "Rutina",
                Estado = "Activa",
                Fecha = DateTime.Now
            };
        }
    }
}
