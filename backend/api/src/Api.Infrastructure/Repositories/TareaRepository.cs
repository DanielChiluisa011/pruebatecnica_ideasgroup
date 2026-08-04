using Api.Domain.Entities;
using Api.Domain.Exceptions;
using Api.Domain.Ports.Out;
using Api.Infrastructure.Persistence;
using Api.Infrastructure.Persistence.Entities;

namespace Api.Infrastructure.Repositories;

public class TareaRepository(AppDbContext context) : ITareaRepository
{
    private readonly AppDbContext _context = context;

    public Task<Tarea> ActualizarTarea(Tarea tarea)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CrearTarea(Tarea tarea)
    {
        try
        {
            var tareaDb = new TareaDb
            {
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                SecuencialColumna = tarea.SecuencialColumna,
                SecuencialPrioridad = tarea.SecuencialPrioridad,
                FechaCreacion = tarea.FechaCreacion,
                SecuencialUsuarioAsignado = tarea.SecuencialUsuarioAsignado,
                EstaActivo = tarea.EstaActivo,
                Orden = tarea.Orden
            };

            _context.Tareas.Add(tareaDb);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al crear la tarea: {ex.Message}");
        }
    }

    public Task<bool> EliminarTarea(int secuencial)
    {
        try
        {
            var tareaDb = _context.Tareas.FirstOrDefault(t => t.Secuencial == secuencial);
            if (tareaDb == null)
            {
                throw new AppException("No existe la tarea buscada.", 404);
            }
            tareaDb.EstaActivo = false;
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al crear la tarea: {ex.Message}");
        }
    }

    public async Task<Tarea> ObtenerTareaPorSecuencial(int secuencial)
    {
        try
        {
            var tareaDb = _context.Tareas.FirstOrDefault(t => t.Secuencial == secuencial);
            if (tareaDb == null)
            {
                throw new AppException("No existe la tarea buscada.", 404);
            }
            return new Tarea(tareaDb.Secuencial, tareaDb.Titulo, tareaDb.Descripcion, tareaDb.SecuencialColumna, tareaDb.SecuencialPrioridad, tareaDb.SecuencialUsuarioAsignado, tareaDb.FechaCreacion, tareaDb.EstaActivo, tareaDb.Orden);
        }catch (Exception ex)
        {
            throw new AppException($"Error al obtener la tarea: {ex.Message}");
        }
    }

    public async Task<List<Tarea>> ObtenerTareasPorColumna(int SecuencialColumna)
    {
        try
        {
            List<Tarea> tareas = new List<Tarea>();
            var tareasDb = _context.Tareas.Where(t => t.SecuencialColumna == SecuencialColumna && t.EstaActivo).ToList();
            foreach (var tareaDb in tareasDb)
            {
                tareas.Add(new Tarea(tareaDb.Secuencial, tareaDb.Titulo, tareaDb.Descripcion, tareaDb.SecuencialColumna, tareaDb.SecuencialPrioridad, tareaDb.SecuencialUsuarioAsignado, tareaDb.FechaCreacion, tareaDb.EstaActivo, tareaDb.Orden));
            }
            return tareas;
        }catch (Exception ex)
        {
            throw new AppException($"Error al obtener la tarea: {ex.Message}");
        }
    }

    public async Task<List<Tarea>> OrdernarTarea(List<Tarea> tareas)
    {
        try
        {
            foreach(var tarea in tareas)
            {
                var tareaDb = _context.Tareas.FirstOrDefault(t => t.Secuencial == tarea.Secuencial);
                if (tareaDb == null)
                {
                    throw new AppException("No existe la tarea buscada.", 404);
                }
                tareaDb.Orden = tarea.Orden;
            }
            await _context.SaveChangesAsync();
            return tareas;
        }catch (Exception ex)
        {
            throw new AppException($"Error al obtener la tarea: {ex.Message}");
        }
    }
}