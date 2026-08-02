namespace Api.Infrastructure.Persistence.Entities;

public class TareaDb
{
    public int Secuencial { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int SecuencialColumna { get; set; }
    public int SecuencialPrioridad { get; set; }
    public int SecuencialUsuarioAsignado { get; set; }
    public int Orden { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public bool EstaActivo { get; set; }
    public ColumnaDb Columna { get; set; } = null!;
    public PrioridadDb Prioridad { get; set; } = null!;
    public UsuarioDb UsuarioAsignado { get; set; } = null!;
}