using System.ComponentModel.DataAnnotations.Schema;
using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.User;

[Table("users",Schema = "public")]
public class User : Entity
{
    [Column("email")]
    public Email? Email { get; private set; }
    
    [Column("name")]
    public string? Name { get; private set; }

    public static User Create(Email email, string name)
    {
        User user = new() { Email = email, Id = Guid.NewGuid(), Name = name };
        return user;
    }
}