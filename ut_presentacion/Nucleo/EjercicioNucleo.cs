using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EjercicioNucleo
    {
        public static Ejercicio Ejercicio()
        {
            return new Ejercicio
            {
                Nombre = "Ejercicio-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                GrupoMuscular = "Piernas",
                Dificultad = "Intermedio",
                ImagenUrl = "/img/ejercicio.jpg",
                Instrucciones = "Instrucciones de prueba"
            };
        }
    }
}
