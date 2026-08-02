using Api.Domain.Entities;

namespace Api.Domain.Ports.Out
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByEmail(string email);
        Task<bool> Register(Usuario usuario);
    }
}