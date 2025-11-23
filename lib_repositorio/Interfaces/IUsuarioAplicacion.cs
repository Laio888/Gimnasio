using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IUsuarioAplicacion
    {
        void Configurar(string StringConexion);
        List<Usuario> PorNombre(Usuario? entidad);
        List<Usuario> Listar();
        Usuario? Guardar(Usuario? entidad);
        Usuario? Modificar(Usuario? entidad);
        Usuario? Borrar(Usuario? entidad);
        Usuario? Login(string correo, string clave);
        Usuario? Registrar(Usuario? entidad);

    }
}
