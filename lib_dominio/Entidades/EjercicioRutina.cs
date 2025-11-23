using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class EjercicioRutina
    {
        public int Id { get; set; }
        public int RutinaId { get; set; }
        public int EjercicioId { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public int Orden { get; set; }
    }
}
