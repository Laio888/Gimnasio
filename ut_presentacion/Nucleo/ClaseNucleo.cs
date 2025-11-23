using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class ClaseNucleo
    {
        public static Clase Clase(int coachId, int horarioId)
        {
            return new Clase
            {
                Nombre = "Clase-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                Duracion = 60,
                Cupos = 20,
                CoachId = coachId,
                HorarioId = horarioId
            };
        }
    }
}