using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{

    public class MensajeNucleo
    {
        public static Mensaje Mensaje(int usuarioId, int coachId)
        {
            return new Mensaje
            {
                UsuarioId = usuarioId,
                CoachId = coachId,
                Contenido = "Mensaje de prueba",
                Estado = "Pendiente",
                Fecha = DateTime.Now
            };
        }
    }
}