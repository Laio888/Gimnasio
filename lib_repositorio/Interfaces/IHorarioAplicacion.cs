using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IHorarioAplicacion
    {
        void Configurar(string StringConexion);
        List<Horario> PorNombre(Horario? entidad);
        List<Horario> Listar();
        Horario? Guardar(Horario? entidad);
        Horario? Modificar(Horario? entidad);
        Horario? Borrar(Horario? entidad);
    }
}