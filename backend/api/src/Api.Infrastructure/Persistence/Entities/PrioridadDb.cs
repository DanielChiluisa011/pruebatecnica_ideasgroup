namespace Api.Infrastructure.Persistence.Entities;

public class PrioridadDb
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool EstaActivo { get; set; } = true;
}