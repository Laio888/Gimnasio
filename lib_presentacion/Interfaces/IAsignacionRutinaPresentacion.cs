using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IAsignacionRutinaPresentacion
    {
        Task<List<AsignacionRutina>> Listar();
        Task<AsignacionRutina?> Guardar(AsignacionRutina? entidad);
        Task<AsignacionRutina?> Modificar(AsignacionRutina? entidad);
        Task<AsignacionRutina?> Borrar(AsignacionRutina? entidad);
    }

}
