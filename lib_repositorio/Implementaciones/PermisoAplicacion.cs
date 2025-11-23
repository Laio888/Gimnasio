using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class PermisoAplicacion : IPermisoAplicacion
    {
        private IConexion? IConexion = null;

        public PermisoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Permiso? Guardar(Permiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Permiso!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Permiso? Modificar(Permiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Permiso>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Permiso? Borrar(Permiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Permiso!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Permiso> Listar()
        {
            return this.IConexion!.Permiso!.Take(50).ToList();
        }

        public List<Permiso> PorNombre(Permiso? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Permiso!.AsQueryable();

            // Filtros dinámicos según los campos de Permiso
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (!string.IsNullOrEmpty(entidad.Descripcion))
                query = query.Where(x => x.Descripcion!.Contains(entidad.Descripcion));

            return query.Take(50).ToList();
        }
    }
}