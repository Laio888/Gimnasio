using lib_dominio.Entidades;
using lib_repositorio.Interfaces;

namespace lib_repositorio.Implementaciones
{
    public class TokenAplicacion
    {
        private IConexion? IConexion = null;
        private string llaveBase = "GymAPIKey"; // Llave base configurable

        public TokenAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        // 🔐 Login de Usuario con validación de credenciales
        public Dictionary<string, object> Llave(Usuario? entidad)
        {
            var respuesta = new Dictionary<string, object>();

            var usuario = this.IConexion!.Usuario!
                .FirstOrDefault(x => x.Nombre == entidad!.Nombre && x.PasswordHash == entidad.PasswordHash);

            if (usuario == null)
            {
                respuesta["Respuesta"] = "Error";
                respuesta["Mensaje"] = "Usuario no encontrado o credenciales inválidas";
                respuesta["Fecha"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return respuesta;
            }

            var token = Guid.NewGuid().ToString();

            respuesta["Respuesta"] = "OK";
            respuesta["Usuario"] = usuario.Nombre;
            respuesta["Correo"] = usuario.Correo;
            respuesta["Llave"] = $"{llaveBase}-{token}";
            respuesta["Fecha"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            this.IConexion!.Auditoria!.Add(new Auditoria
            {
                UsuarioId = usuario.Id,
                Accion = "LOGIN",
                Entidad = "Usuario",
                EntidadId = usuario.Id,
                Fecha = DateTime.Now,
                Detalle = $"Usuario {usuario.Nombre} inició sesión y se generó token."
            });
            this.IConexion.SaveChanges();

            return respuesta;
        }

        // 🔑 Generar token para Usuario
        public string Generar(Usuario usuario)
        {
            return Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{usuario.Correo}:{DateTime.Now}")
            );
        }

        // 🔑 Generar token para Coach
        public string Generar(Coach coach)
        {
            return Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{coach.Correo}:{DateTime.Now}")
            );
        }

        // ✅ Validar token recibido
        public Dictionary<string, object> Validar(Dictionary<string, object> datos)
        {
            var respuesta = new Dictionary<string, object>();

            if (!datos.ContainsKey("Llave"))
            {
                respuesta["Respuesta"] = "Error";
                respuesta["Mensaje"] = "Llave no proporcionada";
                return respuesta;
            }

            var llaveRecibida = datos["Llave"]?.ToString();

            if (string.IsNullOrEmpty(llaveRecibida) || !llaveRecibida.StartsWith(llaveBase))
            {
                respuesta["Respuesta"] = "Error";
                respuesta["Mensaje"] = "Llave inválida";
            }
            else
            {
                respuesta["Respuesta"] = "OK";
                respuesta["Mensaje"] = "Llave válida";
                respuesta["Llave"] = llaveRecibida;
            }

            return respuesta;
        }
    }
}