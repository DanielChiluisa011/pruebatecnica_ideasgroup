using Api.Domain.Entities;
using Api.Domain.Ports.Out;
using Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;
public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Usuario> GetUsuarioByEmail(string email)
    {
        var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);
        if (usuarioDb == null)
        {
            return null;
        }

        return new Usuario(usuarioDb.Secuencial, usuarioDb.Nombre, usuarioDb.Correo, usuarioDb.Password);
    }
}