using Api.Domain.Entities;
using Api.Domain.Exceptions;
using Api.Domain.Ports.In;
using Api.Domain.Ports.Out;

namespace Api.Application.UseCases;

public class ProyectoUseCase : IProyectoUseCase
{
    private readonly IProyectoRepository _proyectoRepository;

    public ProyectoUseCase(IProyectoRepository proyectoRepository)
    {
        _proyectoRepository = proyectoRepository;
    }

    public Task<Proyecto> ActualizarProyecto(Proyecto proyecto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CrearProyecto(Proyecto proyecto)
    {
        var nuevoProyecto = await _proyectoRepository.CrearProyecto(proyecto);
        return true;
    }

    public async Task<bool> CrearProyectoAsync(Proyecto proyecto)
    {
        Proyecto proyectoExistente = await _proyectoRepository.ObtenerProyectoPorSecuencial(proyecto.Secuencial);
        if (proyectoExistente == null)
        {
            throw new AppException("El proyecto ya existe.", 409);
        }
        return await _proyectoRepository.CrearProyecto(proyecto);
    }

    public Task<bool> EliminarProyecto(int secuencial)
    {
        return _proyectoRepository.EliminarProyecto(secuencial);
    }

    public async Task<Proyecto> ObtenerProyectoPorSecuencial(int secuencial)
    {
        var proyecto = await _proyectoRepository.ObtenerProyectoPorSecuencial(secuencial);
        if (proyecto == null)
        {
            throw new AppException("No existe el proyecto buscado.", 404);
        }
        return proyecto;
    }

    public async Task<List<Proyecto>> ObtenerProyectosPorEstado(string estadoCodigo)
    {
        List<Proyecto> proyectos = await _proyectoRepository.ObtenerProyectosPorEstado(estadoCodigo);
        return proyectos;
    }
}