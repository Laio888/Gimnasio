using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class ClaseAplicacion : IClaseAplicacion
    {
        private IConexion? IConexion = null;

        public ClaseAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Clase? Guardar(Clase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Clase!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clase? Modificar(Clase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Clase>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clase? Borrar(Clase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Clase!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clase> Listar()
        {
            return this.IConexion!.Clase!.Take(50).ToList();
        }

        public List<Clase> PorNombre(Clase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Clase!.AsQueryable();

            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (entidad.Duracion > 0)
                query = query.Where(x => x.Duracion == entidad.Duracion);

            if (entidad.Cupos > 0)
                query = query.Where(x => x.Cupos >= entidad.Cupos);

            if (entidad.CoachId != 0)
                query = query.Where(x => x.CoachId == entidad.CoachId);

            if (entidad.HorarioId != 0)
                query = query.Where(x => x.HorarioId == entidad.HorarioId);

            return query.Take(50).ToList();
        }

        public List<Clase> ListarPorFecha(DateTime fecha)
        {
            var query = this.IConexion!.Clase!.AsQueryable();
            return query.Take(50).ToList();
        }
    }
}