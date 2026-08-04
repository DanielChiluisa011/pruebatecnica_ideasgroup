
namespace Api.Domain.Entities;
public class Proyecto
{
    private DateTime fechaInicio;

    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaFin { get; set; }
    public string CodigoEstadoProyecto { get; set; } = string.Empty;
    public List<Proyecto_Usuario> ProyectoUsuarios { get; set; } = new List<Proyecto_Usuario>();
    public EstadoProyecto EstadoProyecto { get; set; } = null!;


    public Proyecto(int secuencial, string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin, string codigoEstadoProyecto, List<Proyecto_Usuario> proyectoUsuarios)
    {
        Secuencial = secuencial;
        Nombre = nombre;
        Descripcion = descripcion;
        this.fechaInicio = fechaInicio;
        FechaFin = fechaFin;
        CodigoEstadoProyecto = codigoEstadoProyecto;
        ProyectoUsuarios = proyectoUsuarios;
    }
}