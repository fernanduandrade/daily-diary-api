using DailyDiary.Domain.Common;
using System.Net.Mail;
namespace DailyDiary.Domain.Users;

public sealed record Email
{
    private Email(string value) => Value = value;
    public string Value { get; }

    public static Result<Email> Create(string? email)
    {
        if(string.IsNullOrEmpty(email))
            return Result.Failure<Email>(EmailErrors.Empty);
        if (!IsValid(email))
            return Result.Failure<Email>(EmailErrors.Invalid);

        return new Email(email);
    }

    private static bool IsValid(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
};

public static class EmailErrors
{
    public static readonly Error Empty = new("Empty", "Email is empty");
    public static readonly Error Invalid = new("Invalid", "Email provided is invalid");
}