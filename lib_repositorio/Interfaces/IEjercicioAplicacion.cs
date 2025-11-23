using lib_dominio.Entidades;

public interface IEjercicioAplicacion
{
    void Configurar(string StringConexion);
    List<Ejercicio> PorNombre(Ejercicio? entidad);
    List<Ejercicio> Listar();
    Ejercicio? Guardar(Ejercicio? entidad);
    Ejercicio? Modificar(Ejercicio? entidad);
    Ejercicio? Borrar(Ejercicio? entidad);
}