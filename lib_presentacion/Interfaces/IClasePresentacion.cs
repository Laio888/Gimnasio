using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IClasePresentacion
    {
        Task<List<Clase>> Listar();
        Task<Clase?> Guardar(Clase? entidad);
        Task<Clase?> Modificar(Clase? entidad);
        Task<Clase?> Borrar(Clase? entidad);
        Task<List<Clase>> ListarPorFecha(DateTime fecha);

    }
}
