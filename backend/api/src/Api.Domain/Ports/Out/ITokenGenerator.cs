using Api.Domain.Entities;

namespace Api.Domain.Ports.Out
{
    public interface ITokenGenerator
    {
        string GenerateToken(Usuario usuario);
    }
}