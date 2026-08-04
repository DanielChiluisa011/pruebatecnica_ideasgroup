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
    public int Orden { get; set; }
    public Columna Columna { get; set; } = null!;
    public Prioridad Prioridad { get; set; } = null!;
    public Usuario UsuarioAsignado { get; set; } = null!;
    public Tarea(int secuencial, string titulo, string descripcion, int secuencialColumna, int secuencialPrioridad, int secuencialUsuarioAsignado, DateTime fechaCreacion, bool estaActivo, int orden)
    {
        Secuencial = secuencial;
        Titulo = titulo;
        Descripcion = descripcion;
        SecuencialColumna = secuencialColumna;
        SecuencialPrioridad = secuencialPrioridad;
        SecuencialUsuarioAsignado = secuencialUsuarioAsignado;
        FechaCreacion = fechaCreacion;
        EstaActivo = estaActivo;
        Orden = orden;
    }
}