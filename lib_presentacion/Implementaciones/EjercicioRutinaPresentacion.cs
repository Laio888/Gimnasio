using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class EjercicioRutinaPresentacion : IEjercicioRutinaPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<EjercicioRutina>> Listar()
        {
            var lista = new List<EjercicioRutina>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "EjercicioRutina/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<EjercicioRutina>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<EjercicioRutina?> Guardar(EjercicioRutina? entidad)
        {
            if (entidad == null || entidad.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "EjercicioRutina/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<EjercicioRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<EjercicioRutina?> Modificar(EjercicioRutina? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "EjercicioRutina/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<EjercicioRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<EjercicioRutina?> Borrar(EjercicioRutina? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "EjercicioRutina/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<EjercicioRutina>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}