using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IMensajePresentacion
    {
        Task<List<Mensaje>> Listar();
        Task<Mensaje?> Guardar(Mensaje? entidad);
        Task<Mensaje?> Modificar(Mensaje? entidad);
        Task<Mensaje?> Borrar(Mensaje? entidad);
    }
}
