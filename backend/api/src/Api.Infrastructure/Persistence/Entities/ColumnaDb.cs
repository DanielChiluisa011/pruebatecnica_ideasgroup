namespace Api.Infrastructure.Persistence.Entities;

public class ColumnaDb
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public int SecuencialProyecto { get; set; }
    public bool EstaActivo { get; set; } = true;
    public ProyectoDb Proyecto { get; set; } = null!;

}