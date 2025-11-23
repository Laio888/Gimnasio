using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IEjercicioPresentacion
    {
        Task<List<Ejercicio>> Listar();
        Task<Ejercicio?> Guardar(Ejercicio? entidad);
        Task<Ejercicio?> Modificar(Ejercicio? entidad);
        Task<Ejercicio?> Borrar(Ejercicio? entidad);
    }
}
