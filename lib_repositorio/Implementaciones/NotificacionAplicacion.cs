using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class NotificacionAplicacion : INotificacionAplicacion
    {
        private IConexion? IConexion = null;

        public NotificacionAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Notificacion? Guardar(Notificacion? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad.Fecha = DateTime.Now; // se asigna fecha automática
            this.IConexion!.Notificacion!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Notificacion? Modificar(Notificacion? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Notificacion>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Notificacion? Borrar(Notificacion? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Notificacion!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Notificacion> Listar()
        {
            return this.IConexion!.Notificacion!.Take(50).ToList();
        }

        public List<Notificacion> PorNombre(Notificacion? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Notificacion!.AsQueryable();

            // Filtros dinámicos según los campos de Notificacion
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (!string.IsNullOrEmpty(entidad.Tipo))
                query = query.Where(x => x.Tipo == entidad.Tipo);

            if (!string.IsNullOrEmpty(entidad.Texto))
                query = query.Where(x => x.Texto!.Contains(entidad.Texto));

            if (entidad.Fecha != DateTime.MinValue)
                query = query.Where(x => x.Fecha.Date == entidad.Fecha.Date);

            if (!string.IsNullOrEmpty(entidad.Estado))
                query = query.Where(x => x.Estado == entidad.Estado);

            return query.Take(50).ToList();
        }
    }
}