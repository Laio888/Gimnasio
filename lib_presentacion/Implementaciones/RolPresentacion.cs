using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;

namespace lib_presentacion.Implementaciones
{
    public class RolPresentacion : IRolPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Rol>> Listar()
        {
            var lista = new List<Rol>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Rol/Listar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            lista = JsonConversor.ConvertirAObjeto<List<Rol>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Rol?> Guardar(Rol? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Rol/Guardar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Rol>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Rol?> Modificar(Rol? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Rol/Modificar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Rol>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Rol?> Borrar(Rol? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Rol/Borrar");
            var respuesta = await comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            entidad = JsonConversor.ConvertirAObjeto<Rol>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public Task<List<Rol>> PorNombre(Rol? filtro)
        {
            throw new NotImplementedException();
        }
    }
}