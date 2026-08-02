using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Api.Infrastructure.Persistence.Entities;

public class ProyectoDb
{
    public int Secuencial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string CodigoEstadoProyecto { get; set; } = string.Empty;

    public EstadoProyectoDb EstadoProyecto { get; set; } = null!;
}