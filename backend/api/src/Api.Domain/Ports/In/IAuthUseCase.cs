namespace Api.Domain.Ports.In;

public interface IAuthUseCase
{
    Task<string> Login(string email, string password);
}