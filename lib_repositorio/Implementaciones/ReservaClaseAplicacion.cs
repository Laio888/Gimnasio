using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class ReservaClaseAplicacion : IReservaClaseAplicacion
    {
        private IConexion? IConexion = null;

        public ReservaClaseAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ReservaClase? Guardar(ReservaClase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ReservaClase!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ReservaClase? Modificar(ReservaClase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<ReservaClase>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ReservaClase? Borrar(ReservaClase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ReservaClase!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ReservaClase> Listar()
        {
            return this.IConexion!.ReservaClase!.Take(50).ToList();
        }

        public List<ReservaClase> PorNombre(ReservaClase? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.ReservaClase!.AsQueryable();

            // Filtros dinámicos según los campos de ReservaClase
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            if (entidad.ClaseId != 0)
                query = query.Where(x => x.ClaseId == entidad.ClaseId);

            if (!string.IsNullOrEmpty(entidad.Estado))
                query = query.Where(x => x.Estado == entidad.Estado);

            if (entidad.FechaReserva != DateTime.MinValue)
                query = query.Where(x => x.FechaReserva >= entidad.FechaReserva);

            return query.Take(50).ToList();
        }
        public ReservaClase? Reservar(int usuarioId, int claseId, DateTime fechaReserva)
        {
            // Validar cupos disponibles
            var clase = this.IConexion!.Clase!.FirstOrDefault(c => c.Id == claseId);
            if (clase == null)
                throw new Exception("Clase no encontrada");

            if (clase.Cupos <= 0)
                return null; // ❌ No hay cupos

            // Crear reserva
            var reserva = new ReservaClase
            {
                UsuarioId = usuarioId,
                ClaseId = claseId,
                FechaReserva = fechaReserva,
                Estado = "Confirmada"
            };

            this.IConexion!.ReservaClase!.Add(reserva);

            // Reducir cupos de la clase
            clase.Cupos -= 1;
            var entry = this.IConexion.Entry<Clase>(clase);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();

            return reserva;
        }
    }
}