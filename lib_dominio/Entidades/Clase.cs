using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public int Cupos { get; set; }
        public int CoachId { get; set; }
        public int HorarioId { get; set; }
    }
}
