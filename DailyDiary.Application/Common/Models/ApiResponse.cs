namespace DailyDiary.Application.Common.Models;

public sealed class ApiResponse<T>
{
    public T Data { get; set;  }
    public string? Message { get; set; }
    public bool Success { get; set; }
}