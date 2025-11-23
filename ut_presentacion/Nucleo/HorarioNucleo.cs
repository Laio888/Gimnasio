using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class HorarioNucleo
    {
        public static Horario Horario(int coachId)
        {
            return new Horario
            {
                DiaSemana = "Lunes",
                HoraInicio = new TimeSpan(8, 0, 0),
                HoraFin = new TimeSpan(9, 0, 0),
                CoachId = coachId
            };
        }
    }
}
