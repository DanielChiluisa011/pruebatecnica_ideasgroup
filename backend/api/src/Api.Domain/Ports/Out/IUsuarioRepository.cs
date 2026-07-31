using Api.Domain.Entities;

namespace Api.Domain.Ports.Out
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}