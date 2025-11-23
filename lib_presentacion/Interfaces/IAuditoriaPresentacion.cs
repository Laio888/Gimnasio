using lib_dominio.Entidades;

namespace lib_presentacion.Interfaces
{
    public interface IAuditoriaPresentacion
    {
        Task<List<Auditoria>> Listar();
        Task<Auditoria?> Guardar(Auditoria? entidad);
        Task<Auditoria?> Modificar(Auditoria? entidad);
        Task<Auditoria?> Borrar(Auditoria? entidad);
    }
}