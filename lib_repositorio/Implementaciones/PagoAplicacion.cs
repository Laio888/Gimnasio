using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class PagoAplicacion : IPagoAplicacion
    {
        private IConexion? IConexion = null;

        public PagoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Pago? Guardar(Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad.Fecha = DateTime.Now; // asigna fecha automática al pago
            this.IConexion!.Pago!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pago? Modificar(Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Pago>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pago? Borrar(Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Pago!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pago> Listar()
        {
            return this.IConexion!.Pago!.Take(50).ToList();
        }

        public List<Pago> PorNombre(Pago? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Pago!.AsQueryable();

            // Filtros dinámicos según los campos de Pago
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (entidad.Monto > 0)
                query = query.Where(x => x.Monto == entidad.Monto);

            if (!string.IsNullOrEmpty(entidad.Metodo))
                query = query.Where(x => x.Metodo == entidad.Metodo);

            if (entidad.Fecha != DateTime.MinValue)
                query = query.Where(x => x.Fecha.Date == entidad.Fecha.Date);

            if (!string.IsNullOrEmpty(entidad.Estado))
                query = query.Where(x => x.Estado == entidad.Estado);

            return query.Take(50).ToList();
        }
    }
}