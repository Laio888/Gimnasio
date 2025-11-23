using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IRolPermisoPresentacion
    {
        Task<List<RolPermiso>> Listar();
        Task<RolPermiso?> Guardar(RolPermiso? entidad);
        Task<RolPermiso?> Modificar(RolPermiso? entidad);
        Task<RolPermiso?> Borrar(RolPermiso? entidad);
    }
}