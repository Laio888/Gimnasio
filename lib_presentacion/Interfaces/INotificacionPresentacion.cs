using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface INotificacionPresentacion
    {
        Task<List<Notificacion>> Listar();
        Task<Notificacion?> Guardar(Notificacion? entidad);
        Task<Notificacion?> Modificar(Notificacion? entidad);
        Task<Notificacion?> Borrar(Notificacion? entidad);
    }
}