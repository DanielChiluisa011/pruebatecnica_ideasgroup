using System.Net;

namespace Api.Domain.Exceptions;

public class AppException : Exception
{
    public int CodigoEstado { get; }
    public AppException(string mensaje, int codigoEstado = (int)HttpStatusCode.BadRequest) : base(mensaje)
    {
        CodigoEstado = codigoEstado;
    }
}