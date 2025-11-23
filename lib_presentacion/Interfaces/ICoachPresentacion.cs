using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface ICoachPresentacion
    {
        Task<List<Coach>> Listar();
        Task<List<Coach>> PorEspecialidad(Coach? entidad);
        Task<Coach?> Guardar(Coach? entidad);
        Task<Coach?> Modificar(Coach? entidad);
        Task<Coach?> Borrar(Coach? entidad);
        Task<Coach?> Login(string correo);
    }
}