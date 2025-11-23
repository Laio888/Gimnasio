using lib_dominio.Entidades;

public class RolPermisoNucleo
{
    public static RolPermiso RolPermiso(int rolId, int permisoId)
    {
        return new RolPermiso
        {
            RolId = rolId,
            PermisoId = permisoId
        };
    }
}
