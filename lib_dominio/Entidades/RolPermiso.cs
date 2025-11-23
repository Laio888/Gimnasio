using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class RolPermiso
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int PermisoId { get; set; }
    }
}
