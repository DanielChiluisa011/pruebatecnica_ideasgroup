using Api.Domain.Entities;

namespace Api.Domain.Ports.Out
{
    public interface ITokenGnerator
    {
        string GenerateToken(Usuario usuario);
    }
}