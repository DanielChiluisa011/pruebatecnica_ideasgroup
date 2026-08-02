using Api.Domain.Entities;
using Api.Domain.Exceptions;
using Api.Domain.Ports.Out;
using Api.Infrastructure.Persistence;
using Api.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class ProyectoRepository(AppDbContext context) : IProyectoRepository
{
    private readonly AppDbContext _context = context;

    public Task<Proyecto> ActualizarProyecto(Proyecto proyecto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CrearProyecto(Proyecto proyecto)
    {
        var proyectoDb = new ProyectoDb
        {
            Secuencial = proyecto.Secuencial,
            Nombre = proyecto.Nombre,
            Descripcion = proyecto.Descripcion,
            FechaInicio = DateTime.SpecifyKind(proyecto.FechaCreacion, DateTimeKind.Utc),
            FechaFin = DateTime.SpecifyKind(proyecto.FechaFin, DateTimeKind.Utc),   
            CodigoEstadoProyecto = proyecto.CodigoEstadoProyecto
        };

        _context.Proyectos.Add(proyectoDb);
        return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }

    public async Task<bool> EliminarProyecto(int secuencial)
    {
        var proyectoDb = await _context.Proyectos.FirstOrDefaultAsync(p => p.Secuencial == secuencial);
        if (proyectoDb == null)
        {
            throw new AppException("No existe el proyecto buscado.", 404);
        }
        proyectoDb.CodigoEstadoProyecto = "I";
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Proyecto> ObtenerProyectoPorSecuencial(int secuencial)
    {
        var proyectoDb = await _context.Proyectos.FirstOrDefaultAsync(p => p.Secuencial == secuencial);
        if (proyectoDb == null)
        {
            throw new AppException("No existe el proyecto buscado.", 404);
        }
        return new Proyecto(proyectoDb.Secuencial, proyectoDb.Nombre, proyectoDb.Descripcion,proyectoDb.FechaInicio, proyectoDb.FechaFin, proyectoDb.CodigoEstadoProyecto);
    }

    public Task<List<Proyecto>> ObtenerProyectosPorEstado(string estadoCodigo)
    {
        List<Proyecto> proyectos = new List<Proyecto>();
        var proyectosDb = _context.Proyectos.Where(p => p.CodigoEstadoProyecto == estadoCodigo).ToList();
        foreach (var proyectoDb in proyectosDb)
        {
            proyectos.Add(new Proyecto(proyectoDb.Secuencial, proyectoDb.Nombre, proyectoDb.Descripcion, proyectoDb.FechaInicio, proyectoDb.FechaFin, proyectoDb.CodigoEstadoProyecto));
        }
        return Task.FromResult(proyectos);
    }
}