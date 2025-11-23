using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IRutinaPresentacion
    {
        Task<List<Rutina>> Listar();
        Task<Rutina?> Guardar(Rutina? entidad);
        Task<Rutina?> Modificar(Rutina? entidad);
        Task<Rutina?> Borrar(Rutina? entidad);
    }
}