using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{

    public interface IAsignacionRutinaAplicacion
    {
        void Configurar(string StringConexion);
        List<AsignacionRutina> PorNombre(AsignacionRutina? entidad);
        List<AsignacionRutina> Listar();
        AsignacionRutina? Guardar(AsignacionRutina? entidad);
        AsignacionRutina? Modificar(AsignacionRutina? entidad);
        AsignacionRutina? Borrar(AsignacionRutina? entidad);
    }
}   