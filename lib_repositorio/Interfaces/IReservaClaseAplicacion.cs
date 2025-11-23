using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IReservaClaseAplicacion
    {
        void Configurar(string StringConexion);
        List<ReservaClase> PorNombre(ReservaClase? entidad);
        List<ReservaClase> Listar();
        ReservaClase? Guardar(ReservaClase? entidad);
        ReservaClase? Modificar(ReservaClase? entidad);
        ReservaClase? Borrar(ReservaClase? entidad);
    }
}