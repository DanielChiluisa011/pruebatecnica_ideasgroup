namespace Api.Domain.Entities;

public class Columna
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public int SecuencialProyecto { get; set; }
    public bool EstaActivo { get; set; } = true;    
    public Proyecto Proyecto { get; set; } = null!;
}