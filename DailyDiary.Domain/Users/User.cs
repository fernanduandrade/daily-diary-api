using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;

namespace DailyDiary.Domain.Users;

public class User : Entity, IAggregateRoot
{
    public Email? Email { get; private set; }
    public Password? Password { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public string? Name { get; private set; }
    
    public ICollection<Diary> Diaries { get; set; } = new List<Diary>();

    public static User Create(Email email, string name, Password password)
    {
        User user = new() { Email = email, Id = Guid.NewGuid(), Name = name, Password = password };
        return user;
    }
}

public static class UserErrors
{
    public static readonly Error EmptyName = new ("Empty Name", "User name can't be empty");
    public static readonly Error InvalidEmail = new ("Invalid email", "Email already exists");
    public static readonly Error NotFound = new ("Not Found", "Email or password not correct");
    public static readonly Error Blocked = new("Blocked", "Usuer blocked, please request to change the password");
}