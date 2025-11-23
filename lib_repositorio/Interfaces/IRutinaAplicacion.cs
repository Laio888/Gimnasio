using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IRutinaAplicacion
    {
        void Configurar(string StringConexion);
        List<Rutina> PorNombre(Rutina? entidad);
        List<Rutina> Listar();
        Rutina? Guardar(Rutina? entidad);
        Rutina? Modificar(Rutina? entidad);
        Rutina? Borrar(Rutina? entidad);
    }
}