using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class MensajeAplicacion : IMensajeAplicacion
    {
        private IConexion? IConexion = null;

        public MensajeAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Mensaje? Guardar(Mensaje? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad.Fecha = DateTime.Now; // se asigna fecha automática
            this.IConexion!.Mensaje!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Mensaje? Modificar(Mensaje? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Mensaje>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Mensaje? Borrar(Mensaje? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Mensaje!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Mensaje> Listar()
        {
            return this.IConexion!.Mensaje!.Take(50).ToList();
        }

        public List<Mensaje> PorNombre(Mensaje? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Mensaje!.AsQueryable();

            // Filtros dinámicos según los campos de Mensaje
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (!string.IsNullOrEmpty(entidad.Contenido))
                query = query.Where(x => x.Contenido!.Contains(entidad.Contenido));

            if (!string.IsNullOrEmpty(entidad.Estado))
                query = query.Where(x => x.Estado == entidad.Estado);

            if (entidad.Fecha != DateTime.MinValue)
                query = query.Where(x => x.Fecha.Date == entidad.Fecha.Date);

            return query.Take(50).ToList();
        }
    }
}