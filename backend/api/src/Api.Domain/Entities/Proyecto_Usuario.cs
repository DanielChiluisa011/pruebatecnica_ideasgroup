namespace Api.Domain.Entities;

public class Proyecto_Usuario
{
    public int Secuencial { get; set; }
    public int SecuencialProyecto { get; set; }
    public int SecuencialUsuario { get; set; }
    public bool EstaActivo { get; set; } = true;
}