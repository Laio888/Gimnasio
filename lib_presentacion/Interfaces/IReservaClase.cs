using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IReservaClasePresentacion
    {
        Task<List<ReservaClase>> Listar();
        Task<ReservaClase?> Guardar(ReservaClase? entidad);
        Task<ReservaClase?> Modificar(ReservaClase? entidad);
        Task<ReservaClase?> Borrar(ReservaClase? entidad);
    }
}