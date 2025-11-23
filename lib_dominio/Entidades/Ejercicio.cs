using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Ejercicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? GrupoMuscular { get; set; }
        public string? Dificultad { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Instrucciones { get; set; }
    }
}
