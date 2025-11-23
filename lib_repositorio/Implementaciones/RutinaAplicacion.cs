using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class RutinaAplicacion : IRutinaAplicacion
    {
        private IConexion? IConexion = null;

        public RutinaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Rutina? Guardar(Rutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Rutina!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Rutina? Modificar(Rutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Rutina>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Rutina? Borrar(Rutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Rutina!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Rutina> Listar()
        {
            return this.IConexion!.Rutina!.Take(50).ToList();
        }

        public List<Rutina> PorNombre(Rutina? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Rutina!.AsQueryable();

            // Filtros dinámicos según los campos de Rutina
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (!string.IsNullOrEmpty(entidad.Nivel))
                query = query.Where(x => x.Nivel == entidad.Nivel);

            if (entidad.DuracionMin > 0)
                query = query.Where(x => x.DuracionMin >= entidad.DuracionMin);

            if (entidad.UsuarioId != 0)
                query = query.Where(x => x.UsuarioId == entidad.UsuarioId);

            return query.Take(50).ToList();
        }
    }
}