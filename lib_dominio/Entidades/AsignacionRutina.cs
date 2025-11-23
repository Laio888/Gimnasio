namespace lib_dominio.Entidades
{
    public class AsignacionRutina
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RutinaId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
