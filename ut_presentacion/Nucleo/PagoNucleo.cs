using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class PagoNucleo
    {
        public static Pago Pago(int usuarioId)
        {
            return new Pago
            {
                UsuarioId = usuarioId,
                Monto = 100.00m,
                Metodo = "Tarjeta",
                Estado = "Pendiente",
                Fecha = DateTime.Now
            };
        }
    }
}

