using Api.Domain.Entities;

namespace Api.Domain.Ports.In;

public interface IAuthUseCase
{
    Task<Usuario> Login(string email, string password);
    Task<bool> Register(string Nombre, string email, string password);
}