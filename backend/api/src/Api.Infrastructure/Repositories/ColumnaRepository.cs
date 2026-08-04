using Api.Domain.Entities;
using Api.Domain.Exceptions;
using Api.Domain.Ports.Out;
using Api.Infrastructure.Persistence;
using Api.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class ColumnaRepository(AppDbContext context) : IColumnaRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Columna> ActualizarColumna(Columna columna)
    {
        try
        {
            var columnaDb = _context.Columnas.FirstOrDefault(c => c.Secuencial == columna.Secuencial);
            if(columnaDb == null)
            {
                throw new AppException("No existe la columna buscada.", 404);
            }
            columnaDb.Nombre = columna.Nombre;
            columnaDb.Orden = columna.Orden;
            columnaDb.SecuencialProyecto = columna.SecuencialProyecto;
            columnaDb.EstaActivo = columna.EstaActivo;
            await _context.SaveChangesAsync();
            return columna;
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al crear la columna: {ex.Message}");
        }
    }

    public async Task<bool> CrearColumna(Columna columna)
    {
        try
        {
            var columnaDb = new ColumnaDb
            {
                Nombre = columna.Nombre,
                Orden = columna.Orden,
                SecuencialProyecto = columna.SecuencialProyecto,
                EstaActivo = columna.EstaActivo
            };

            _context.Columnas.Add(columnaDb);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al crear la columna: {ex.Message}");
        }
    }

    public async Task<bool> EliminarColumna(int secuencial)
    {
        try
        {
            var columnaDb = await _context.Columnas.FirstOrDefaultAsync(c => c.Secuencial == secuencial);
            if (columnaDb == null)
            {
                throw new AppException("No existe la columna buscada.", 404);
            }
            columnaDb.EstaActivo = false;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al eliminar la columna: {ex.Message}");
        }
    }

    public async Task<Columna> ObtenerColumnaPorId(int secuencial)
    {
        try
        {
            var columnaDb = await _context.Columnas.FirstOrDefaultAsync(c => c.Secuencial == secuencial);
            if (columnaDb == null)
            {
                throw new AppException("No existe la columna buscada.", 404);
            }
            return new Columna
            {
                Secuencial = columnaDb.Secuencial,
                Nombre = columnaDb.Nombre,
                Orden = columnaDb.Orden,
                SecuencialProyecto = columnaDb.SecuencialProyecto,
                EstaActivo = columnaDb.EstaActivo
            };
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al obtener la columna: {ex.Message}");
        }
    }

    public async Task<List<Columna>> ObtenerColumnasPorProyecto(int secuencialProyecto)
    {
        try
        {            
            List<Columna> columnas = await _context.Columnas
                .Where(c => c.SecuencialProyecto == secuencialProyecto && c.EstaActivo)
                .Select(c => new Columna
                {
                    Secuencial = c.Secuencial,
                    Nombre = c.Nombre,
                    Orden = c.Orden,
                    SecuencialProyecto = c.SecuencialProyecto,
                    EstaActivo = c.EstaActivo
                })
                .ToListAsync();
            return columnas;
        }
        catch (Exception ex)
        {
            throw new AppException($"Error al obtener la columna: {ex.Message}");
        }
    }
}