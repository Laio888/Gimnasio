using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface INotificacionAplicacion
    {
        void Configurar(string StringConexion);
        List<Notificacion> PorNombre(Notificacion? entidad);
        List<Notificacion> Listar();
        Notificacion? Guardar(Notificacion? entidad);
        Notificacion? Modificar(Notificacion? entidad);
        Notificacion? Borrar(Notificacion? entidad);
    }
}