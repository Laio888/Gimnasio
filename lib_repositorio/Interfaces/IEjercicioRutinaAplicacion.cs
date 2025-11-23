using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{

    public interface IEjercicioRutinaAplicacion
    {
        void Configurar(string StringConexion);
        List<EjercicioRutina> PorNombre(EjercicioRutina? entidad);
        List<EjercicioRutina> Listar();
        EjercicioRutina? Guardar(EjercicioRutina? entidad);
        EjercicioRutina? Modificar(EjercicioRutina? entidad);
        EjercicioRutina? Borrar(EjercicioRutina? entidad);
    }
}