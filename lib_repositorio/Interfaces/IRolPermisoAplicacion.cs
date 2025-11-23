using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IRolPermisoAplicacion
    {
        void Configurar(string StringConexion);
        List<RolPermiso> PorNombre(RolPermiso? entidad);
        List<RolPermiso> Listar();
        RolPermiso? Guardar(RolPermiso? entidad);
        RolPermiso? Modificar(RolPermiso? entidad);
        RolPermiso? Borrar(RolPermiso? entidad);
    }
}