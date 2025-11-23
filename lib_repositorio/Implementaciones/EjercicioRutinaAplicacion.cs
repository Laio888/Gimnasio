using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class EjercicioRutinaAplicacion : IEjercicioRutinaAplicacion
    {
        private IConexion? IConexion = null;

        public EjercicioRutinaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public EjercicioRutina? Guardar(EjercicioRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.EjercicioRutina!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EjercicioRutina? Modificar(EjercicioRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<EjercicioRutina>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EjercicioRutina? Borrar(EjercicioRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.EjercicioRutina!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<EjercicioRutina> Listar()
        {
            return this.IConexion!.EjercicioRutina!.Take(50).ToList();
        }

        public List<EjercicioRutina> PorNombre(EjercicioRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.EjercicioRutina!.AsQueryable();

            // Filtros dinámicos según los campos de EjercicioRutina
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.RutinaId != 0)
                query = query.Where(x => x.RutinaId == entidad.RutinaId);

            if (entidad.EjercicioId != 0)
                query = query.Where(x => x.EjercicioId == entidad.EjercicioId);

            if (entidad.Series > 0)
                query = query.Where(x => x.Series == entidad.Series);

            if (entidad.Repeticiones > 0)
                query = query.Where(x => x.Repeticiones == entidad.Repeticiones);

            if (entidad.Orden > 0)
                query = query.Where(x => x.Orden == entidad.Orden);

            return query.Take(50).ToList();
        }
    }
}