using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IEjercicioRutinaPresentacion
    {
        Task<List<EjercicioRutina>> Listar();
        Task<EjercicioRutina?> Guardar(EjercicioRutina? entidad);
        Task<EjercicioRutina?> Modificar(EjercicioRutina? entidad);
        Task<EjercicioRutina?> Borrar(EjercicioRutina? entidad);
    }

}
