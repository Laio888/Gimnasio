using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IHorarioPresentacion
    {
        Task<List<Horario>> Listar();
        Task<Horario?> Guardar(Horario? entidad);
        Task<Horario?> Modificar(Horario? entidad);
        Task<Horario?> Borrar(Horario? entidad);
    }
}