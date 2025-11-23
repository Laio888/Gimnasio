using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Coach
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Especialidad { get; set; }
        public string? FotoUrl { get; set; }
        public int Experiencia { get; set; }
        public string Correo { get; set; } = string.Empty;
    }
}
