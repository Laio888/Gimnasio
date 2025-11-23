using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class EjercicioAplicacion : IEjercicioAplicacion
    {
        private IConexion? IConexion = null;

        public EjercicioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Ejercicio? Guardar(Ejercicio? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Ejercicio!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ejercicio? Modificar(Ejercicio? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Ejercicio>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ejercicio? Borrar(Ejercicio? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Ejercicio!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Ejercicio> Listar()
        {
            return this.IConexion!.Ejercicio!.Take(50).ToList();
        }

        public List<Ejercicio> PorNombre(Ejercicio? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Ejercicio!.AsQueryable();

            // Filtros dinámicos según los campos de Ejercicio
            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (!string.IsNullOrEmpty(entidad.GrupoMuscular))
                query = query.Where(x => x.GrupoMuscular!.Contains(entidad.GrupoMuscular));

            if (!string.IsNullOrEmpty(entidad.Dificultad))
                query = query.Where(x => x.Dificultad == entidad.Dificultad);

            return query.Take(50).ToList();
        }
    }
}