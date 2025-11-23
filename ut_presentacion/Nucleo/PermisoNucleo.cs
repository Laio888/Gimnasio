using lib_dominio.Entidades;

public class PermisoNucleo
{
    public static Permiso Permiso()
    {
        return new Permiso
        {
            Nombre = "Permiso-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
            Descripcion = "Permiso de prueba"
        };
    }
}
