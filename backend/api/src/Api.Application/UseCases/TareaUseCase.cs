using Api.Domain.Entities;
using Api.Domain.Ports.In;

namespace Api.Application.UseCases;

public class TareaUseCase : ITareaUseCase
{
    private readonly ITareaUseCase _tareaUseCase;

    public TareaUseCase(ITareaUseCase tareaUseCase)
    {
        _tareaUseCase = tareaUseCase;
    }

    public async Task<bool> CrearTarea(Tarea tarea)
    {
        var result = await _tareaUseCase.CrearTarea(tarea);
        return result;
    }

    public async Task<bool> EliminarTarea(int secuencial)
    {
        return await _tareaUseCase.EliminarTarea(secuencial);
    }

    public async Task<Tarea> ObtenerTareaPorSecuencial(int secuencial)
    {
        return await _tareaUseCase.ObtenerTareaPorSecuencial(secuencial);
    }

    public async Task<List<Tarea>> ObtenerTareasPorColumna(int SecuencialColumna)
    {
        return await _tareaUseCase.ObtenerTareasPorColumna(SecuencialColumna);
    }

    public async Task<Tarea> ActualizarTarea(Tarea tarea)
    {
        return await _tareaUseCase.ActualizarTarea(tarea);
    }

    public async Task<List<Tarea>> OrdernarTarea(List<Tarea> tareas)
    {
        return await _tareaUseCase.OrdernarTarea(tareas);
    }
}