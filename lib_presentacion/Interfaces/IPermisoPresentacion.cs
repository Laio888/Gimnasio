using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IPermisoPresentacion
    {
        Task<List<Permiso>> Listar();
        Task<Permiso?> Guardar(Permiso? entidad);
        Task<Permiso?> Modificar(Permiso? entidad);
        Task<Permiso?> Borrar(Permiso? entidad);
    }
}