using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IRolAplicacion
    {
        void Configurar(string StringConexion);
        List<Rol> PorNombre(Rol? entidad);
        List<Rol> Listar();
        Rol? Guardar(Rol? entidad);
        Rol? Modificar(Rol? entidad);
        Rol? Borrar(Rol? entidad);
    }
}