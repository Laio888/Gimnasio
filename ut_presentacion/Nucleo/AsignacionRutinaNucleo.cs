using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class AsignacionRutinaNucleo
    {
        public static AsignacionRutina AsignacionRutina(int usuarioId, int rutinaId)
        {
            return new AsignacionRutina
            {
                UsuarioId = usuarioId,
                RutinaId = rutinaId,
                Estado = "Activa",
                FechaAsignacion = DateTime.Now
            };
        }
    }
}
