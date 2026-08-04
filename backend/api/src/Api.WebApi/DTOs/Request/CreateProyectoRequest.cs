namespace Api.WebApi.DTOs.Request;

public record CreateProyectoRequest(int Secuencial, string Nombre, string Descripcion, DateTime FechaInicio, DateTime FechaFin);