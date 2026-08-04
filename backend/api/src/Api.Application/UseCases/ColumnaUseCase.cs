using Api.Domain.Entities;
using Api.Domain.Ports.In;
using Api.Domain.Ports.Out;

namespace Api.Application.UseCases;

public class ColumnaUseCase : IColumnaUseCase
{
    private readonly IColumnaRepository _columnaRepository;

    public ColumnaUseCase(IColumnaRepository columnaRepository)
    {
        _columnaRepository = columnaRepository;
    }

    public async Task<Columna> ActualizarColumna(Columna Columna)
    {
        Columna columna = await _columnaRepository.ActualizarColumna(Columna);
        return columna;
    }

    public Task<bool> CrearColumna(Columna Columna)
    {
        var columna = _columnaRepository.CrearColumna(Columna);
        return columna;
    }

    public Task<bool> EliminarColumna(int secuencial)
    {
        var columna = _columnaRepository.EliminarColumna(secuencial);
        return columna;
    }

    public Task<Columna> ObtenerColumnaPorSecuencial(int secuencial)
    {
        var columna = _columnaRepository.ObtenerColumnaPorId(secuencial);
        return columna;
    }

    public Task<List<Columna>> ObtenerColumnasPorProyecto(int SecuencialProyecto)
    {
        var columnas = _columnaRepository.ObtenerColumnasPorProyecto(SecuencialProyecto);
        return columnas;
    }
}