using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class RolPermisoAplicacion : IRolPermisoAplicacion
    {
        private IConexion? IConexion = null;

        public RolPermisoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public RolPermiso? Guardar(RolPermiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.RolPermiso!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public RolPermiso? Modificar(RolPermiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<RolPermiso>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public RolPermiso? Borrar(RolPermiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.RolPermiso!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<RolPermiso> Listar()
        {
            return this.IConexion!.RolPermiso!.Take(50).ToList();
        }

        public List<RolPermiso> PorNombre(RolPermiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.RolPermiso!.AsQueryable();

            // Filtros dinámicos según los campos de RolPermiso
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.RolId != 0)
                query = query.Where(x => x.RolId == entidad.RolId);

            if (entidad.PermisoId != 0)
                query = query.Where(x => x.PermisoId == entidad.PermisoId);

            return query.Take(50).ToList();
        }
    }
}