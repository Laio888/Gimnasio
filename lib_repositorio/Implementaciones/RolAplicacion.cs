using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class RolAplicacion : IRolAplicacion
    {
        private IConexion? IConexion = null;

        public RolAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Rol? Guardar(Rol? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Rol!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Rol? Modificar(Rol? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Rol>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Rol? Borrar(Rol? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Rol!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Rol> Listar()
        {
            return this.IConexion!.Rol!.Take(50).ToList();
        }

        public List<Rol> PorNombre(Rol? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Rol!.AsQueryable();

            // Filtros dinámicos según los campos de Rol
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