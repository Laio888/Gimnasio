using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IAuditoriaAplicacion
    {
        void Configurar(string StringConexion);
        List<Auditoria> PorNombre(Auditoria? entidad);
        List<Auditoria> Listar();
        Auditoria? Guardar(Auditoria? entidad);
        Auditoria? Modificar(Auditoria? entidad);
        Auditoria? Borrar(Auditoria? entidad);
    }
}
