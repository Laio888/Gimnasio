using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public int EntidadId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Detalle { get; set; }
    }
}
