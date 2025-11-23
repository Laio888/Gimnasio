using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EjercicioRutinaNucleo
    {
        public static EjercicioRutina EjercicioRutina(int rutinaId, int ejercicioId)
        {
            return new EjercicioRutina
            {
                RutinaId = rutinaId,
                EjercicioId = ejercicioId,
                Series = 3,
                Repeticiones = 12,
                Orden = 1
            };
        }
    }
}
