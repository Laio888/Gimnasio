using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class ReservaClase
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ClaseId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaReserva { get; set; }
    }
}
