using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Pago
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Metodo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
