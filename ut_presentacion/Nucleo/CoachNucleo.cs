using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class CoachNucleo
    {
        public static Coach Coach()
        {
            var entidad = new Coach();
            entidad.Nombre = "Coach-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entidad.Especialidad = "Entrenamiento Funcional";
            entidad.FotoUrl = "/img/pruebas.jpg";
            entidad.Experiencia = 3;
            entidad.Correo = "coach" + DateTime.Now.ToString("yyyyMMddhhmmss") + "@gym.com";
            return entidad;
        }
    }
}