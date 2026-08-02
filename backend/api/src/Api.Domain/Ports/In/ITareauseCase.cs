using Api.Domain.Entities;

namespace Api.Domain.Ports.In;

public interface ITarea
{
    Task<bool> CrearTarea(Tarea tarea);
    Task<bool> EliminarTarea(int secuencial);
    Task<Tarea> ObtenerTareaPorSecuencial(int secuencial);
    Task<List<Tarea>> ObtenerTareasPorColumna(int SecuencialColumna);
    Task<Tarea> ActualizarTarea(Tarea tarea);
}