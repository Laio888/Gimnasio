using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class ReservaClaseNucleo
    {
        public static ReservaClase ReservaClase(int usuarioId, int claseId)
        {
            return new ReservaClase
            {
                UsuarioId = usuarioId,
                ClaseId = claseId,
                Estado = "Pendiente",
                FechaReserva = DateTime.Now
            };
        }
    }
}