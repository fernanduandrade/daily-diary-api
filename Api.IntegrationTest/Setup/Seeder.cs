using DailyDiary.Domain.User;
using DailyDiary.Infrastructure.Persistence.Data;

namespace Api.IntegrationTest.Setup;

public class Seeder
{
    private readonly AppDbContext _dbContext;

    public Seeder(AppDbContext dbContext)
        => (_dbContext) = (dbContext);
    public void AddData()
    {
        var email = Email.Create("usertest@tester.com").Value;
        var password = Password.Create("Juazeiro0.").Value;
        var user = User.Create(email, "user test", password);
        user.Id = new Guid("5b359013-c291-4e89-9274-877dfeb85d02");
        var genericEmail = Email.Create("joaozin123@tester.com").Value;
        var genericPassword = Password.Create("passwordtest").Value;
        var genericUser = User.Create(genericEmail, "joao tester", genericPassword);
        _dbContext.Users.AddRange(new List<User> {user, genericUser});
        _dbContext.SaveChanges();
    }
}