using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using FluentAssertions;

namespace Domain.UniTest.Users;

public class UserTest
{

    [Fact(DisplayName = "Should create user when is valid.")]
    [Trait("DomainTest", "UserTest")]
    public void Should_CreateUser()
    {
        // arrange
        var email = Email.Create("testuser@gmail.com").Value;
        var password = Password.Create("joaozin123").Value;
        
        // act
        var user = User.Create(email, "user test", password);

        // assert
        user.Email.Should().NotBeNull();
        user.Password.Should().NotBeNull();
        user.Name.Should().NotBeNull();
    }
    
    [Fact(DisplayName = "Should create user with constructor properties.")]
    [Trait("DomainTest", "UserTest")]
    public void User_Should_CreateWith_ConstructorProperties()
    {
        // arrange
        var email = Email.Create("testuser@gmail.com").Value;
        var password = Password.Create("joaozin123").Value;
        
        // act
        var user = User.Create(email, "user test", password);

        // assert
        user.Email.Value.Should().Be("testuser@gmail.com");
        user.Name.Should().Be("user test");
    }

    [Fact(DisplayName = "Raise a domain event should increment domain list event")]
    [Trait("DomainTest", "UserTest")]
    public void RaiseAEvent_Should_IncrementDomainEventList()
    {
        // arrange
        var email = Email.Create("testuser@gmail.com").Value;
        var password = Password.Create("joaozin123").Value;
        
        // act
        var user = User.Create(email, "user test", password);
        user.Raise(new CreatedUserEvent());
        
        // assert
        user.DomainEvents.Count.Should().Be(1);
    }
}


public class CreatedUserEvent : IDomainEvent {}