namespace Api.Infrastructure.Persistence.Entities;

public class EstadoProyectoDb
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public bool EstaActivo { get; set; } = true;
}