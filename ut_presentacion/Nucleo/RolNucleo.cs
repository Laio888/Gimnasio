using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class RolNucleo
    {
        public static Rol Rol()
        {
            return new Rol
            {
                Nombre = "Rol-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                Descripcion = "Rol de prueba"
            };
        }
    }
}