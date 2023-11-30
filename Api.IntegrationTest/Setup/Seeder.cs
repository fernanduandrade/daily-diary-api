using Bogus;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Users;
using DailyDiary.Infrastructure.Persistence.Data;

namespace Api.IntegrationTest.Setup;

public class Seeder
{
    private readonly AppDbContext _dbContext;

    public Seeder(AppDbContext dbContext)
        => (_dbContext) = (dbContext);
    public void AddData()
    {
        #region UserData
        var user = GenerateUser("user test", "usertest@tester.com", "Juazeiro0.");
        user.Id = new Guid("5b359013-c291-4e89-9274-877dfeb85d02");

        var genericUser = GenerateUser("joao tester",
            "joaozin123@tester.com",
            "passwordtest");
        genericUser.Id = new Guid("8636d1c9-e331-4da6-959f-d2133f754fda");
        _dbContext.Users.AddRange(new List<User> {user, genericUser});
        #endregion

        #region DiaryData
        var diary = Diary.Create("Today was hard", "content", "sad", genericUser.Id);
        var genericDiary = Diary.Create("Today was hard", "content", "sad", genericUser.Id);
        var genericDiary2 = Diary.Create("Hard", "content", "sad", genericUser.Id);
        genericDiary.Id = new Guid("4510804c-4d88-4916-bfab-a37e13e32760");
        genericDiary2.Id = new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc");
        _dbContext.Diaries.AddRange(new List<Diary>() { diary, genericDiary, genericDiary2 });
        #endregion
        
        _dbContext.SaveChanges();
    }
    
    
    private User GenerateUser(string nameInput, string emailInput, string passwordInput)
    {
        var id = new Faker().Random.Guid();
        Email email = Email.Create(emailInput).Value;
        Password password = Password.Create(passwordInput).Value;
        var user = User.Create(email, nameInput, password);
        user.Id = id;
        return user;
    }
}