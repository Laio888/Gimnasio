using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class ReservaClasePresentacion : IReservaClasePresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<ReservaClase>> Listar()
        {
            var lista = new List<ReservaClase>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "ReservaClase/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<ReservaClase>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        // Guardar nueva reserva (usa UsuarioId + ClaseId + FechaReserva)
        public async Task<ReservaClase?> Guardar(ReservaClase? entidad)
        {
            if (entidad == null || entidad.UsuarioId <= 0 || entidad.ClaseId <= 0 || entidad.FechaReserva == default)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "ReservaClase/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<ReservaClase>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        // Modificar una reserva existente (según tu API, normalmente cambia Estado o FechaReserva)
        public async Task<ReservaClase?> Modificar(ReservaClase? entidad)
        {
            if (entidad == null || entidad.UsuarioId <= 0 || entidad.ClaseId <= 0 || entidad.FechaReserva == default)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "ReservaClase/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<ReservaClase>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        // Borrar una reserva (identificada por UsuarioId + ClaseId + FechaReserva)
        public async Task<ReservaClase?> Borrar(ReservaClase? entidad)
        {
            if (entidad == null || entidad.UsuarioId <= 0 || entidad.ClaseId <= 0 || entidad.FechaReserva == default)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "ReservaClase/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<ReservaClase>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        // Reservar clase con validación de cupos en el backend
        public async Task<ReservaClase?> Reservar(int usuarioId, int claseId, DateTime fecha)
        {
            var entidad = new ReservaClase
            {
                UsuarioId = usuarioId,
                ClaseId = claseId,
                FechaReserva = fecha,
                Estado = "Confirmada"
            };

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "ReservaClase/Reservar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<ReservaClase>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}