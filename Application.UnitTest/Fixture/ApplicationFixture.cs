using AutoMapper;
using Bogus;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.User;
using Xunit;

namespace Application.UnitTest.Fixture;

public class ApplicationFixture : IDisposable
{
    public User GenerateUser(string nameInput, string emailInput, string passwordInput)
    {
        var id = new Faker().Random.Guid();
        Email email = Email.Create(emailInput).Value;
        Password password = Password.Create(passwordInput).Value;
        var user = User.Create(email, nameInput, password);
        user.Id = id;
        return user;
    }
    
    public UserDto CreateUserDto(User user)
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        var mapper = mapperConfig.CreateMapper();
        var dto = mapper.Map<UserDto>(user);
        return dto;
    }

    public Guid GetRandomGuid()
    {
        var guid = new Faker().Random.Guid();
        return guid;
    }
    public void Dispose()
    {
        // TODO release managed resources here
    }
}

[CollectionDefinition(nameof(CollectionApplicationFixture))]
public class CollectionApplicationFixture : ICollectionFixture<ApplicationFixture> {}