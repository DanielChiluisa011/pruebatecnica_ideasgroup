namespace Api.Domain.Ports.In;

public interface IAuthUseCase
{
    Task<string> Login(string email, string password);
    Task<bool> Register(string Nombre, string email, string password);
}