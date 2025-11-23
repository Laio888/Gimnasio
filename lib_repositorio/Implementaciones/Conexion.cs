using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Usuario>? Usuario { get; set; }
        public DbSet<Rol>? Rol { get; set; }
        public DbSet<Permiso>? Permiso { get; set; }
        public DbSet<RolPermiso>? RolPermiso { get; set; }
        public DbSet<Coach>? Coach { get; set; }
        public DbSet<Rutina>? Rutina { get; set; }
        public DbSet<Ejercicio>? Ejercicio { get; set; }
        public DbSet<EjercicioRutina>? EjercicioRutina { get; set; }
        public DbSet<AsignacionRutina>? AsignacionRutina { get; set; }
        public DbSet<Horario>? Horario { get; set; }
        public DbSet<Clase>? Clase { get; set; }
        public DbSet<ReservaClase>? ReservaClase { get; set; }
        public DbSet<Mensaje>? Mensaje { get; set; }
        public DbSet<Notificacion>? Notificacion { get; set; }
        public DbSet<Pago>? Pago { get; set; }
        public DbSet<Auditoria>? Auditoria { get; set; }
    }
}