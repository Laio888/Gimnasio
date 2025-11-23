using lib_dominio.Entidades;

namespace lib_repositorio.Interfaces
{
    public interface IPagoAplicacion
    {
        void Configurar(string StringConexion);
        List<Pago> PorNombre(Pago? entidad);
        List<Pago> Listar();
        Pago? Guardar(Pago? entidad);
        Pago? Modificar(Pago? entidad);
        Pago? Borrar(Pago? entidad);
    }
}