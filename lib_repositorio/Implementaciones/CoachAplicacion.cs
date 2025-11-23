using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class CoachAplicacion : ICoachAplicacion
    {
        private IConexion? IConexion = null;

        public CoachAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Coach? Guardar(Coach? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Coach!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Coach? Modificar(Coach? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Coach>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Coach? Borrar(Coach? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Coach!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Coach> Listar()
        {
            return this.IConexion!.Coach!.Take(50).ToList();
        }

        public List<Coach> PorNombre(Coach? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Coach!.AsQueryable();

            // Filtros dinámicos según los campos de Coach
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (!string.IsNullOrEmpty(entidad.Especialidad))
                query = query.Where(x => x.Especialidad!.Contains(entidad.Especialidad));

            if (!string.IsNullOrEmpty(entidad.Correo))
                query = query.Where(x => x.Correo == entidad.Correo);

            if (entidad.Experiencia > 0)
                query = query.Where(x => x.Experiencia >= entidad.Experiencia);

            return query.Take(50).ToList();
        }
        public Coach? Login(string correo)
        {
            if (string.IsNullOrEmpty(correo))
                throw new Exception("lbFaltaInformacion");

            var coach = this.IConexion!.Coach!
                .FirstOrDefault(x => x.Correo == correo);

            return coach;
        }
    }
}