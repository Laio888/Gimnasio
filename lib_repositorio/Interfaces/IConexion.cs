using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorio.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Usuario>? Usuario { get; set; }
        DbSet<Rol>? Rol { get; set; }
        DbSet<Permiso>? Permiso { get; set; }
        DbSet<RolPermiso>? RolPermiso { get; set; }
        DbSet<Coach>? Coach { get; set; }
        DbSet<Rutina>? Rutina { get; set; }
        DbSet<Ejercicio>? Ejercicio { get; set; }
        DbSet<EjercicioRutina>? EjercicioRutina { get; set; }
        DbSet<AsignacionRutina>? AsignacionRutina { get; set; }
        DbSet<Horario>? Horario { get; set; }
        DbSet<Clase>? Clase { get; set; }
        DbSet<ReservaClase>? ReservaClase { get; set; }
        DbSet<Mensaje>? Mensaje { get; set; }
        DbSet<Notificacion>? Notificacion { get; set; }
        DbSet<Pago>? Pago { get; set; }
        DbSet<Auditoria>? Auditoria { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
