using DailyDiary.Domain.User;
using FluentAssertions;

namespace Domain.UniTest.Users;

public class UserTest
{

    [Fact(DisplayName = "Should create user when email is valid.")]
    [Trait("DailyDiary", "DomainTest")]
    public void Should_CreateUser_When_EmailIsValid()
    {
        // arrange
        var email = Email.Create("testuser@gmail.com").Value;
        
        // act
        var user = User.Create(email, "user test");

        // assert
        user.Email.Should().NotBeNull();
    }
    
    
    
    
}