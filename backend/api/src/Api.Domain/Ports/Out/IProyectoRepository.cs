using Api.Domain.Entities;

namespace Api.Domain.Ports.Out
{
    public interface IProyectoRepository
    {
    Task<bool> CrearProyecto(Proyecto proyecto);
    Task<bool> EliminarProyecto(int secuencial);
    Task<Proyecto> ObtenerProyectoPorSecuencial(int secuencial);
    Task<List<Proyecto>> ObtenerProyectosPorEstado(string estadoCodigo);
    Task<Proyecto> ActualizarProyecto(Proyecto proyecto);
    }
}