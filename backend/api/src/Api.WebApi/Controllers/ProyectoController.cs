using Microsoft.AspNetCore.Mvc;
using Api.Domain.Ports.In;
using Api.WebApi.DTOs;
using Api.WebApi.DTOs.Request;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Api.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProyectoController : ControllerBase
{
    private readonly IProyectoUseCase _proyectoUseCase;

    public ProyectoController(IProyectoUseCase proyectoUseCase)
    {
        _proyectoUseCase = proyectoUseCase;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearProyecto([FromBody] CreateProyectoRequest request)
    {
        try
        {
            var proyecto = new Proyecto(0, request.Nombre, request.Descripcion, request.FechaInicio, request.FechaFin,"A", new List<Proyecto_Usuario>());

            var result = await _proyectoUseCase.CrearProyecto(proyecto);
            if (result)
            {
                return Ok(new { message = "Proyecto creado exitosamente." });
            }
            else
            {
                return BadRequest(new { message = "Error al crear el proyecto." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpGet("{secuencial}")]
    public async Task<IActionResult> ObtenerProyectoPorSecuencial(int secuencial)
    {
        try
        {
            var proyecto = await _proyectoUseCase.ObtenerProyectoPorSecuencial(secuencial);
            if (proyecto != null)
            {
                return Ok(proyecto);
            }
            else
            {
                return NotFound(new { message = "No se encontró el proyecto." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpGet("usuario/{secuencialUsuario}")]
    public async Task<IActionResult> ObtenerProyectosPorUsuario(int secuencialUsuario)
    {
        try
        {
            var proyectos = await _proyectoUseCase.ObtenerProyectosPorUsuario(secuencialUsuario);
            return Ok(new {proyectos = proyectos});
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    [HttpPut("eliminar/{secuencial}")]
    public async Task<IActionResult> EliminarProyecto(int secuencial)
    {
        try
        {
            var result = await _proyectoUseCase.EliminarProyecto(secuencial);
            if (result)
            {
                return Ok(new { message = "Proyecto eliminado exitosamente." });
            }
            else
            {
                return NotFound(new { message = "No se encontró el proyecto." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("actualizar")]
    public async Task<IActionResult> ActualizarProyecto([FromBody] UpdateProyectoRequest request)
    {
        try
        {
            var proyecto = new Proyecto(request.Secuencial, request.Nombre, request.Descripcion, request.FechaInicio, request.FechaFin, request.CodigoEstadoProyecto, request.ProyectoUsuario);
            var updatedProyecto = await _proyectoUseCase.ActualizarProyecto(proyecto);
            if (updatedProyecto != null)
            {
                return Ok(updatedProyecto);
            }
            else
            {
                return NotFound(new { message = "No se encontró el proyecto." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}