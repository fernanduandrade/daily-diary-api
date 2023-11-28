using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.User;

public class User : Entity
{
    public Email? Email { get; private set; }
    public Password? Password { get; private set; }
    
    public string? Name { get; private set; }

    public static User Create(Email email, string name, Password password)
    {
        User user = new() { Email = email, Id = Guid.NewGuid(), Name = name, Password = password};
        return user;
    }
}

public static class UserErrors
{
    public static readonly Error EmptyName = new ("Empty Name", "User name can't be empty");
    public static readonly Error InvalidEmail = new ("Invalid email", "Email already exists");
    public static readonly Error NotFound = new ("Not Found", "Email or password not correct");
}