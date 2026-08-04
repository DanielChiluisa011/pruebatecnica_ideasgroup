using Api.Domain.Entities;

namespace Api.Domain.Ports.In;

public interface ITareaUseCase
{
    Task<bool> CrearTarea(Tarea tarea);
    Task<bool> EliminarTarea(int secuencial);
    Task<Tarea> ObtenerTareaPorSecuencial(int secuencial);
    Task<List<Tarea>> ObtenerTareasPorColumna(int SecuencialColumna);
    Task<Tarea> ActualizarTarea(Tarea tarea);
    Task<List<Tarea>> OrdernarTarea(List<Tarea> tareas);
}