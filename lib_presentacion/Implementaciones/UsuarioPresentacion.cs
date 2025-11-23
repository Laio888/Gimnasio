using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class UsuarioPresentacion : IUsuarioPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Usuario>> Listar()
        {
            var lista = new List<Usuario>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Usuario>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Usuario>> PorNombre(Usuario? entidad)
        {
            var lista = new List<Usuario>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/PorNombre");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Usuario>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Usuario?> Guardar(Usuario? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Usuario?> Modificar(Usuario? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Usuario?> Borrar(Usuario? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Usuario?> Login(string correo, string clave)
        {
            try
            {
                var datos = new Dictionary<string, object>();
                datos["Entidad"] = new
                {
                    Correo = correo,
                    PasswordHash = clave
                };

                comunicaciones = new Comunicaciones();
                datos = comunicaciones.ConstruirUrl(datos, "Usuario/Login");

                var respuesta = await comunicaciones!.Ejecutar(datos);

                if (respuesta == null)
                    throw new Exception("Error: no hubo respuesta del servidor.");

                if (respuesta.ContainsKey("Error"))
                {
                    var error = respuesta["Error"]?.ToString() ?? "Error desconocido";
                    Console.WriteLine($"[BACKEND ERROR] {error}");
                    throw new Exception(error);
                }

                if (!respuesta.ContainsKey("Usuario"))
                    throw new Exception("El servidor no devolvió datos de usuario.");

                var usuarioJson = JsonConversor.ConvertirAString(respuesta["Usuario"]);
                var usuario = JsonConversor.ConvertirAObjeto<Usuario>(usuarioJson);

                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOGIN PRESENTACION ERROR] {ex.Message}");
                throw;
            }
        }


        public async Task<Usuario?> Registrar(Usuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Usuario/Registrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString());

            // ✔ El backend devuelve "Usuario", NO "Entidad"
            if (!respuesta.ContainsKey("Usuario"))
                throw new Exception("El servidor no devolvió un usuario válido.");

            var usuarioJson = JsonConversor.ConvertirAString(respuesta["Usuario"]);
            var usuario = JsonConversor.ConvertirAObjeto<Usuario>(usuarioJson);

            return usuario;
        }


    }
}