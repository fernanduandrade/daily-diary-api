using DailyDiary.Domain.Common;
using BC = BCrypt.Net;

namespace DailyDiary.Domain.Users;


public record Password
{
    private const int WorkFactor = 12;
    private Password(string value) => Value = value;
    public string Value { get; }

    public static bool Verify(string input, string hash)
    {
        var validate = BC.BCrypt.Verify(input, hash);
        return validate;
    }

    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<Password>(PasswordErrors.Empty);

        var password = BC.BCrypt.HashPassword(value, WorkFactor);

        return new Password(password);
    }
};

public static class PasswordErrors
{
    public static readonly Error Empty = new("Empty", "Password can't be empty.");
    public static readonly Error Wrong = new("Wrong", "Password is incorrect");
}