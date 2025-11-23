using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IPermisoAplicacion
    {
        void Configurar(string StringConexion);
        List<Permiso> PorNombre(Permiso? entidad);
        List<Permiso> Listar();
        Permiso? Guardar(Permiso? entidad);
        Permiso? Modificar(Permiso? entidad);
        Permiso? Borrar(Permiso? entidad);
    }
}