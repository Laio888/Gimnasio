using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class AsignacionRutinaAplicacion : IAsignacionRutinaAplicacion
    {
        private IConexion? IConexion = null;

        public AsignacionRutinaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public AsignacionRutina? Guardar(AsignacionRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.AsignacionRutina!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public AsignacionRutina? Modificar(AsignacionRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<AsignacionRutina>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public AsignacionRutina? Borrar(AsignacionRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.AsignacionRutina!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<AsignacionRutina> Listar()
        {
            return this.IConexion!.AsignacionRutina!.Take(50).ToList();
        }

        public List<AsignacionRutina> PorNombre(AsignacionRutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.AsignacionRutina!.AsQueryable();

            // Filtros dinámicos según los campos de AsignacionRutina
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (entidad.RutinaId != 0)
                query = query.Where(x => x.RutinaId == entidad.RutinaId);

            if (!string.IsNullOrEmpty(entidad.Estado))
                query = query.Where(x => x.Estado == entidad.Estado);

            if (entidad.FechaAsignacion != DateTime.MinValue)
                query = query.Where(x => x.FechaAsignacion >= entidad.FechaAsignacion);

            return query.Take(50).ToList();
        }
    }
}