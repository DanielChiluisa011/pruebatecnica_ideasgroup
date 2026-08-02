namespace Api.WebApi.DTOs.Request;

public record CreateProyectoRequest(string Nombre, string Descripcion, DateTime FechaInicio, DateTime FechaFin);