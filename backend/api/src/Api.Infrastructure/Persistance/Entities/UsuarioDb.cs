namespace Api.Infrastructure.Persistance.Entities;

public class UsuarioDb
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
}