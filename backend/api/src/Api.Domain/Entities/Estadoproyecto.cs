namespace Api.Domain.Entities;

public class EstadoProyecto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public bool EstaActivo { get; set; } = true;
}