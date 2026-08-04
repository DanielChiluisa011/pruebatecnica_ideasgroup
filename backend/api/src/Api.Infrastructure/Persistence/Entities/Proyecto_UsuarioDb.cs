namespace Api.Infrastructure.Persistence.Entities;

public class Proyecto_UsuarioDb
{
    public int Secuencial { get; set; }
    public int SecuencialProyecto { get; set; }
    public int SecuencialUsuario { get; set; }
    public bool EstaActivo { get; set; } = true;
    public ProyectoDb Proyecto { get; set; } = null!;
    public UsuarioDb Usuario { get; set; } = null!;
}