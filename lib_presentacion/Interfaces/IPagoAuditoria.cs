using lib_dominio.Entidades;
    
    namespace lib_presentacion.Interfaces
{
    public interface IPagoPresentacion
    {
        Task<List<Pago>> Listar();
        Task<Pago?> Guardar(Pago? entidad);
        Task<Pago?> Modificar(Pago? entidad);
        Task<Pago?> Borrar(Pago? entidad);
    }
}
