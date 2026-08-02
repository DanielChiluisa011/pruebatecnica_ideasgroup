namespace Api.Domain.Entities;

public class Tarea
{
    public int Secuencial{ get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int SecuencialColumna { get; set; }
    public int SecuencialPrioridad { get; set; }
    public int SecuencialUsuarioAsignado { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public bool EstaActivo { get; set; }
    public Columna Columna { get; set; } = null!;
    public Prioridad Prioridad { get; set; } = null!;
    public Usuario UsuarioAsignado { get; set; } = null!;
}