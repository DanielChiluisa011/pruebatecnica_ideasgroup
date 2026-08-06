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

    public async Task<Proyecto> ActualizarProyecto(Proyecto proyecto)
    {
        var proyectoDb = await _context.Proyectos.FirstOrDefaultAsync(p => p.Secuencial == proyecto.Secuencial);
        if (proyectoDb == null)
        {
            throw new AppException("No existe el proyecto buscado.", 404);
        }
        proyectoDb.Nombre = proyecto.Nombre;
        proyectoDb.Descripcion = proyecto.Descripcion;
        proyectoDb.FechaInicio = DateTime.SpecifyKind(proyecto.FechaCreacion, DateTimeKind.Utc);
        proyectoDb.FechaFin = DateTime.SpecifyKind(proyecto.FechaFin, DateTimeKind.Utc);
        proyectoDb.CodigoEstadoProyecto = proyecto.CodigoEstadoProyecto;
        await _context.SaveChangesAsync();
        return proyecto;
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
        List<Proyecto_Usuario> proyectoUsuarios = await _context.Proyecto_Usuario
            .Where(pu => pu.SecuencialProyecto == secuencial && pu.EstaActivo)
            .Select(pu => new Proyecto_Usuario
            {
                Secuencial = pu.Secuencial,
                SecuencialProyecto = pu.SecuencialProyecto,
                SecuencialUsuario = pu.SecuencialUsuario,
                EstaActivo = pu.EstaActivo
            })
            .ToListAsync();
        return new Proyecto(proyectoDb.Secuencial, proyectoDb.Nombre, proyectoDb.Descripcion,proyectoDb.FechaInicio, proyectoDb.FechaFin, proyectoDb.CodigoEstadoProyecto, proyectoUsuarios);
    }

    public Task<List<Proyecto>> ObtenerProyectosPorUsuario(int secuencialUsuario)
    {
        List<Proyecto> proyectos = new List<Proyecto>();
        var proyecto_UsuarioDb = _context.Proyecto_Usuario.Where(p => p.SecuencialUsuario == secuencialUsuario);
        proyecto_UsuarioDb.ForEachAsync(p =>
        {
            var proyectosDb = _context.Proyectos.Where(q => q.Secuencial == p.SecuencialProyecto && q.CodigoEstadoProyecto != "I").ToList();    
            
            proyectosDb.ForEach(i =>
            {
                proyectos.Add(new Proyecto(i.Secuencial,i.Nombre, i.Descripcion,i.FechaInicio,i.FechaFin,i.CodigoEstadoProyecto,new List<Proyecto_Usuario>()));
            });
        });
        
        return Task.FromResult(proyectos);
    }
}