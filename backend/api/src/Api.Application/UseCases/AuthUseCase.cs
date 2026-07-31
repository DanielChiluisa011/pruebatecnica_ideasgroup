using Api.Domain.Ports.In;
using Api.Domain.Ports.Out;

namespace Api.Application.UseCases;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGnerator _tokenGenerator;

    public AuthUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher, ITokenGnerator tokenGenerator)
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
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return _tokenGenerator.GenerateToken(usuario);
    }
}