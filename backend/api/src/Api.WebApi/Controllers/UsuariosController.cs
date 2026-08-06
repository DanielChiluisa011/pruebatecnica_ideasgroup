using Microsoft.AspNetCore.Mvc;
using Api.Domain.Ports.In;
using Api.WebApi.DTOs;

namespace Api.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    
    public UsuariosController(IAuthUseCase authUseCase)
    {
        _authUseCase = authUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _authUseCase.Register(request.Nombre,request.Correo, request.Password);
            if (result)
            {
                return Ok(new { message = "User registered successfully." });
            }
            else
            {
                return BadRequest(new { message = "User registration failed." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authUseCase.Login(request.Correo, request.Password);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}