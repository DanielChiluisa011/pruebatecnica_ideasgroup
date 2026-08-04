using Api.Domain.Entities;

namespace Api.Domain.Ports.Out;

public interface IColumnaRepository
{
    Task<bool> CrearColumna(Columna columna);
    Task<Columna> ActualizarColumna(Columna columna);
    Task<bool> EliminarColumna(int secuencial);
    Task<Columna> ObtenerColumnaPorId(int secuencial);
    Task<List<Columna>> ObtenerColumnasPorProyecto(int secuencialProyecto);
}