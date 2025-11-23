using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class PagoPresentacion : IPagoPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Pago>> Listar()
        {
            var lista = new List<Pago>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Pago/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Pago>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Pago?> Guardar(Pago? entidad)
        {
            if (entidad == null || entidad.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Pago/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Pago>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Pago?> Modificar(Pago? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Pago/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Pago>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Pago?> Borrar(Pago? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Pago/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Pago>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}