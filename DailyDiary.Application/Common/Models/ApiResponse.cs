namespace DailyDiary.Application.Common.Models;

public sealed class ApiResponse<T>
{
    public T? Data { get; set;  }
    public string? Message { get; set; }
    public bool Success { get; set; }

    
    public static ApiResponse<T> Response(T data, string message = "Success", bool isSuccess = true)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Message = message,
            Success = isSuccess
        };
    }
}