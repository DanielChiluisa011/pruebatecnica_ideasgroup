namespace Api.WebApi.Common;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }

    public static ApiResponse<T> Ok(T data, string? mensaje = null) => 
    new() {Success = true, Data = data, Message = mensaje};

    public static ApiResponse<T> Fail(string mensaje) => 
    new() {Success = false, Data = default, Message = mensaje};
}

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public static ApiResponse Ok(string? message = null) =>
        new() { Success = true, Message = message };

    public static ApiResponse Fail(string message) =>
        new() { Success = false, Message = message };
}