namespace Api.Domain.Entities;
public class Proyecto
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaFin { get; set; }
    public string CodigoEstadoProyecto { get; set; } = string.Empty;
    public EstadoProyecto EstadoProyecto { get; set; } = null!;
}