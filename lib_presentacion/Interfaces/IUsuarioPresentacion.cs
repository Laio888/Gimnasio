using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IUsuarioPresentacion
    {
        Task<List<Usuario>> Listar();
        Task<List<Usuario>> PorNombre(Usuario? entidad);
        Task<Usuario?> Guardar(Usuario? entidad);
        Task<Usuario?> Modificar(Usuario? entidad);
        Task<Usuario?> Borrar(Usuario? entidad);
        Task<Usuario?> Login(string correo, string clave);
        Task<Usuario?> Registrar(Usuario? entidad);
    }
}