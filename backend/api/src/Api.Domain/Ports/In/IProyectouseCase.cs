using Api.Domain.Entities;

namespace Api.Domain.Ports.In;

public interface IProyectoUseCase
{
    Task<bool> CrearProyecto(Proyecto proyecto);
    Task<bool> EliminarProyecto(int secuencial);
    Task<Proyecto> ObtenerProyectoPorSecuencial(int secuencial);
    Task<List<Proyecto>> ObtenerProyectosPorEstado(string estadoCodigo);
    Task<Proyecto> ActualizarProyecto(Proyecto proyecto);
}