using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public class UsuarioAplicacion : IUsuarioAplicacion
    {
        private IConexion? IConexion = null;

        public UsuarioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Usuario? Guardar(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Usuario!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuario? Modificar(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Usuario>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuario? Borrar(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Usuario!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Usuario> Listar()
        {
            return this.IConexion!.Usuario!.Take(50).ToList();
        }

        public List<Usuario> PorNombre(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var query = this.IConexion!.Usuario!.AsQueryable();

            if (entidad.Id != 0)
                query = query.Where(x => x.Id == entidad.Id);

            if (!string.IsNullOrEmpty(entidad.Nombre))
                query = query.Where(x => x.Nombre!.Contains(entidad.Nombre));

            if (!string.IsNullOrEmpty(entidad.Correo))
                query = query.Where(x => x.Correo == entidad.Correo);

            if (entidad.RolId != 0)
                query = query.Where(x => x.RolId == entidad.RolId);

            return query.Take(50).ToList();
        }

        public Usuario? Login(string correo, string clave)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(clave))
                throw new Exception("lbFaltaInformacion");

            var usuario = this.IConexion!.Usuario!
                .FirstOrDefault(x => x.Correo == correo);

            if (usuario == null)
                return null;

            if (usuario.PasswordHash != clave)
                return null;

            return usuario;
        }

        public Usuario? Registrar(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (string.IsNullOrEmpty(entidad.Correo) || string.IsNullOrEmpty(entidad.PasswordHash))
                throw new Exception("lbFaltaInformacion");

            // Validar que el correo no exista ya
            var existente = this.IConexion!.Usuario!
                .FirstOrDefault(x => x.Correo == entidad.Correo);

            if (existente != null)
                throw new Exception("lbCorreoYaRegistrado");

            // Asignar rol por defecto si no viene
            if (entidad.RolId == 0)
                entidad.RolId = 1;

            if (entidad.FechaRegistro == default(DateTime))
                entidad.FechaRegistro = DateTime.Now;

            // Guardar usuario
            this.IConexion!.Usuario!.Add(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

    }
}