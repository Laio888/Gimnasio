using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class HorarioAplicacion : IHorarioAplicacion
    {
        private IConexion? IConexion = null;

        public HorarioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Horario? Guardar(Horario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Horario!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Horario? Modificar(Horario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Horario>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Horario? Borrar(Horario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Horario!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Horario> Listar()
        {
            return this.IConexion!.Horario!.Take(50).ToList();
        }

        public List<Horario> PorNombre(Horario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Horario!.AsQueryable();

            // Filtros dinámicos según los campos de Horario
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.DiaSemana))
                query = query.Where(x => x.DiaSemana == entidad.DiaSemana);

            if (entidad.CoachId != 0)
                query = query.Where(x => x.CoachId == entidad.CoachId);

            if (entidad.HoraInicio != TimeSpan.Zero)
                query = query.Where(x => x.HoraInicio >= entidad.HoraInicio);

            if (entidad.HoraFin != TimeSpan.Zero)
                query = query.Where(x => x.HoraFin <= entidad.HoraFin);

            return query.Take(50).ToList();
        }
    }
}