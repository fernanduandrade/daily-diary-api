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