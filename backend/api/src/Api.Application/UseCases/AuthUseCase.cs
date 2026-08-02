using Api.Domain.Ports.In;
using Api.Domain.Ports.Out;
using Api.Domain.Entities;
using Api.Domain.Exceptions;
using System.Net;

namespace Api.Application.UseCases;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Login(string email, string password)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmail(email);
        if (usuario == null || !_passwordHasher.VerifyPassword(usuario.Password, password))
        {
            throw new AppException("Correo y/o contraseña incorrectos.", (int)HttpStatusCode.Conflict);
        }

        return _tokenGenerator.GenerateToken(usuario);
    }

    public async Task<bool> Register(string nombre, string email, string password)
    {
        var existingUser = await _usuarioRepository.GetUsuarioByEmail(email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        var hashedPassword = _passwordHasher.HashPassword(password);
        var newUser = new Usuario(0, email, email, hashedPassword); 
        return await _usuarioRepository.Register(newUser);
    }
}