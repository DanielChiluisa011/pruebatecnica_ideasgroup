// src/Api.WebApi/Middleware/ExceptionMiddleware.cs
using System.Net;
using System.Text.Json;
using Api.Domain.Exceptions;
using Api.WebApi.Common;

namespace Api.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ManejarExcepcionAsync(context, ex);
        }
    }

    private async Task ManejarExcepcionAsync(HttpContext context, Exception ex)
    {
        int codigoEstado;
        string mensaje;

        if (ex is AppException appEx)
        {
            // Cualquier AppException lanzada en cualquier parte del proyecto cae aquí
            codigoEstado = appEx.CodigoEstado;
            mensaje = appEx.Message;
            _logger.LogWarning(ex, "Error de negocio [{CodigoEstado}]: {Mensaje}", codigoEstado, mensaje);
        }
        else
        {
            // Cualquier otra excepción no anticipada
            codigoEstado = (int)HttpStatusCode.InternalServerError;
            mensaje = _env.IsDevelopment() ? ex.Message : "Ocurrió un error interno. Contacte al administrador.";
            _logger.LogError(ex, "Error no controlado: {Mensaje}", ex.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = codigoEstado;

        var respuesta = ApiResponse.Fail(mensaje);
        var opciones = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta, opciones));
    }
}