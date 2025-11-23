using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IRolPresentacion
    {
        Task<List<Rol>> Listar();
        Task<Rol?> Guardar(Rol? entidad);
        Task<Rol?> Modificar(Rol? entidad);
        Task<Rol?> Borrar(Rol? entidad);
        Task<List<Rol>> PorNombre(Rol? filtro);
    }
}