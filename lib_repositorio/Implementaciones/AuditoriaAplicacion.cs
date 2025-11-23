using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class AuditoriaAplicacion : IAuditoriaAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Auditoria? Guardar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad.Fecha = DateTime.Now; // fecha automática
            this.IConexion!.Auditoria!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Auditoria? Modificar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Auditoria>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Auditoria? Borrar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Auditoria!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Auditoria> Listar()
        {
            return this.IConexion!.Auditoria!.Take(50).ToList();
        }

        public List<Auditoria> PorNombre(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Auditoria!.AsQueryable();

            // Filtros dinámicos según los campos de Auditoria
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (!string.IsNullOrEmpty(entidad.Accion))
                query = query.Where(x => x.Accion == entidad.Accion);

            if (!string.IsNullOrEmpty(entidad.Entidad))
                query = query.Where(x => x.Entidad == entidad.Entidad);

            if (entidad.EntidadId != 0)
                query = query.Where(x => x.EntidadId == entidad.EntidadId);

            if (entidad.Fecha != DateTime.MinValue)
                query = query.Where(x => x.Fecha.Date == entidad.Fecha.Date);

            return query.Take(50).ToList();
        }
    }
}