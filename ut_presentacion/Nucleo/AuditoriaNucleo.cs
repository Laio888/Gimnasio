using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class AuditoriaNucleo
    {
        public static Auditoria Auditoria(int usuarioId, string entidad, int entidadId)
        {
            return new Auditoria
            {
                UsuarioId = usuarioId,
                Accion = "INSERT",
                Entidad = entidad,
                EntidadId = entidadId,
                Fecha = DateTime.Now,
                Detalle = "Auditoría de prueba"
            };
        }
    }
}
