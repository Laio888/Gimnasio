using asp_servicio.Nucleo;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorio.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using lib_repositorio.Interfaces;

namespace asp_servicio.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioAplicacion? iAplicacion = null;
        private TokenAplicacion? iAplicacionToken = null;

        public UsuarioController(IUsuarioAplicacion? iAplicacion, TokenAplicacion iAplicacionToken)
        {
            this.iAplicacion = iAplicacion;
            this.iAplicacionToken = iAplicacionToken;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
            if (string.IsNullOrEmpty(datos))
                datos = "{}";
            return JsonConversor.ConvertirAObjeto(datos);
        }

        [HttpPost]
        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                var resultado = iAplicacionToken!.Validar(datos);

                if (resultado["Respuesta"]?.ToString() == "Error")
                {
                    respuesta["Error"] = resultado["Mensaje"]?.ToString() ?? "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                respuesta["Entidades"] = this.iAplicacion!.Listar();
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string PorNombre()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }*/
                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                respuesta["Entidades"] = this.iAplicacion!.PorNombre(entidad);
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Guardar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }*/
                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                entidad = this.iAplicacion!.Guardar(entidad);
                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Modificar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }*/
                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                entidad = this.iAplicacion!.Modificar(entidad);
                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Borrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }*/
                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));


                entidad = this.iAplicacion!.Borrar(entidad);
                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
        [HttpPost]
        public string Login()
        {
            var respuesta = new Dictionary<string, object>();

            try
            {
                var datos = ObtenerDatos();

                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                Console.WriteLine("===== BACKEND RECIBIÓ =====");
                Console.WriteLine($"Correo recibido: {entidad.Correo}");
                Console.WriteLine($"PasswordHash recibido: {entidad.PasswordHash}");
                Console.WriteLine("===========================");

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                var usuario = this.iAplicacion!.Login(entidad.Correo, entidad.PasswordHash);

                if (usuario == null)
                {
                    respuesta["Respuesta"] = "Error";
                    respuesta["Error"] = "Credenciales inválidas";
                }
                else
                {
                    var token = iAplicacionToken!.Generar(usuario);

                    respuesta["Respuesta"] = "OK";
                    respuesta["Usuario"] = usuario;
                    respuesta["Token"] = token;
                    respuesta["Fecha"] = DateTime.Now.ToString();
                }

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Registrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();

                // Convertir el JSON recibido a objeto Usuario
                var entidad = JsonConversor.ConvertirAObjeto<Usuario>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));

                // Usar el nuevo método Registrar de UsuarioAplicacion
                var usuario = this.iAplicacion!.Registrar(entidad);

                if (usuario == null)
                {
                    respuesta["Respuesta"] = "Error";
                    respuesta["Error"] = "No se pudo registrar el usuario";
                }
                else
                {
                    // Opcional: generar token inmediatamente después de registrar
                    var token = iAplicacionToken!.Generar(usuario);

                    respuesta["Respuesta"] = "OK";
                    respuesta["Usuario"] = usuario;
                    respuesta["Token"] = token;
                    respuesta["Fecha"] = DateTime.Now.ToString();
                }

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                respuesta["Respuesta"] = "Error";
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
    }
}