using Api.Domain.Entities;
using Api.Domain.Ports.Out;
using Api.Infrastructure.Persistence.Entities;
using Api.Infrastructure.Persistence;
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


    public Task<bool> Register(Usuario usuario)
    {
        var usuarioDb = new UsuarioDb
        {
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Password = usuario.Password
        };

        _context.Usuarios.Add(usuarioDb);
        return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }
}