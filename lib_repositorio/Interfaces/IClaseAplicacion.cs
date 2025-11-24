using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IClaseAplicacion
    {
        void Configurar(string StringConexion);
        List<Clase> PorNombre(Clase? entidad);
        List<Clase> ListarPorFecha(DateTime fecha);
        List<Clase> Listar();
        Clase? Guardar(Clase? entidad);
        Clase? Modificar(Clase? entidad);
        Clase? Borrar(Clase? entidad);
    }
}