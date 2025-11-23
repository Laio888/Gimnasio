using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IMensajeAplicacion
    {
        void Configurar(string StringConexion);
        List<Mensaje> PorNombre(Mensaje? entidad);
        List<Mensaje> Listar();
        Mensaje? Guardar(Mensaje? entidad);
        Mensaje? Modificar(Mensaje? entidad);
        Mensaje? Borrar(Mensaje? entidad);
    }
}