using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class AsignacionRutinaPresentacion : IAsignacionRutinaPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<AsignacionRutina>> Listar()
        {
            var lista = new List<AsignacionRutina>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "AsignacionRutina/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<AsignacionRutina>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<AsignacionRutina?> Guardar(AsignacionRutina? entidad)
        {
            if (entidad == null || entidad.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "AsignacionRutina/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<AsignacionRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<AsignacionRutina?> Modificar(AsignacionRutina? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "AsignacionRutina/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<AsignacionRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<AsignacionRutina?> Borrar(AsignacionRutina? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "AsignacionRutina/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<AsignacionRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}