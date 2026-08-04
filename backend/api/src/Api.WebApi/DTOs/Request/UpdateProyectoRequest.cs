using Api.Domain.Entities;

namespace Api.WebApi.DTOs.Request;

public record UpdateProyectoRequest(int Secuencial, string Nombre, string Descripcion, DateTime FechaInicio, DateTime FechaFin, string CodigoEstadoProyecto, List<Proyecto_Usuario> ProyectoUsuario);