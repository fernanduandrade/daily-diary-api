using DailyDiary.Domain.User;
using FluentAssertions;

namespace Domain.UniTest.Users;

public class EmailTest
{
    [Fact(DisplayName = "Should not create email when is invalid.")]
    [Trait("Domain", "EmailTest")]
    public void Should_Not_CreateEmail_When_IsInValid()
    {
        // arrange
        
        // act
        Action action = () =>
        {
            _ = Email.Create("testmail.com").Value;
        };

        // assert
        action.Should().Throw<InvalidOperationException>();
    }
    
    [Fact(DisplayName = "Should not create email when is empty.")]
    [Trait("Domain", "EmailTest")]
    public void Should_Not_CreateEmail_When_IsEmpty()
    {
        // arrange
        
        // act
        Action action = () =>
        {
            _ = Email.Create("").Value;
        };

        // assert
        action.Should().Throw<InvalidOperationException>();
    }
    
    [Fact(DisplayName = "Should not create email when is empty.")]
    [Trait("Domain", "EmailTest")]
    public void Should_CreateEmail_When_IsValid()
    {
        // arrange
        string emailInput = "testuser@gmail.com";
        
        // act
        Email email = Email.Create(emailInput).Value;
        
        // assert
        email.Value.Should().NotBeNull();
    }
    
}