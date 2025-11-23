using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class AuditoriaPresentacion : IAuditoriaPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Auditoria>> Listar()
        {
            var lista = new List<Auditoria>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Auditoria/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Auditoria>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Auditoria?> Guardar(Auditoria? entidad)
        {
            if (entidad == null || entidad.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Auditoria/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Auditoria>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Auditoria?> Modificar(Auditoria? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Auditoria/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Auditoria>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Auditoria?> Borrar(Auditoria? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Auditoria/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"]!.ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Auditoria>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}