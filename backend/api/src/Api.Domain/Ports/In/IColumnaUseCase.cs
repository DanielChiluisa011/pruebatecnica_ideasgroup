using Api.Domain.Entities;

namespace Api.Domain.Ports.In;

public interface IColumnaUseCase
{
    Task<bool> CrearColumna(Columna Columna);
    Task<bool> EliminarColumna(int secuencial);
    Task<Columna> ObtenerColumnaPorSecuencial(int secuencial);
    Task<List<Columna>> ObtenerColumnasPorProyecto(int SecuencialProyecto);
    Task<Columna> ActualizarColumna(Columna Columna);
}