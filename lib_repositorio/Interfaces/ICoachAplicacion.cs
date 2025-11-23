using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface ICoachAplicacion
    {
        void Configurar(string StringConexion);
        List<Coach> PorNombre(Coach? entidad);
        List<Coach> Listar();
        Coach? Guardar(Coach? entidad);
        Coach? Modificar(Coach? entidad);
        Coach? Borrar(Coach? entidad);
        Coach? Login(string correo);

    }
}