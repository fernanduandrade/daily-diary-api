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
        var email = Email.Create("usertest@tester.com").Value;
        var password = Password.Create("Juazeiro0.").Value;
        var user = User.Create(email, "user test", password);
        user.Id = new Guid("5b359013-c291-4e89-9274-877dfeb85d02");
        var genericEmail = Email.Create("joaozin123@tester.com").Value;
        var genericPassword = Password.Create("passwordtest").Value;
        var genericUser = User.Create(genericEmail, "joao tester", genericPassword);
        genericUser.Id = new Guid("8636d1c9-e331-4da6-959f-d2133f754fda");
        _dbContext.Users.AddRange(new List<User> {user, genericUser});
        #endregion

        #region DiaryData
        var diary = Diary.Create("Today was hard", "content", "sad", genericUser.Id);
        _dbContext.Diaries.Add(diary);
        #endregion
        
        
        _dbContext.SaveChanges();
    }
}