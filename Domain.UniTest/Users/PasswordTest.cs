using DailyDiary.Domain.Users;
using FluentAssertions;

namespace Domain.UniTest.Users;

public class PasswordTest
{

    [Fact(DisplayName = "Can't create password when the value is empty.")]
    [Trait("DailyDiary ", "DomainTest")]
    public void Should_Not_CreatePassword_WhenValue_IsEmpty()
    {
        // arrange
        string input = string.Empty;
        
        // act
        Action action = () =>
        {
            _ = Password.Create(input).Value;
        };
        
        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Should return true if password is correct")]
    [Trait("Domain", "PasswordTest")]
    public void Should_BeEqualTo_True_IfPassword_IsCorrect()
    {
        // arrange
        var input = "joaozin123";
        var hashInput = "$2a$12$77jHhW..GkrYmNFuVvR.y.XGVxkfAfaKkdCyWaSOrAMRMPOqGbIn6";

        // act
        var result = Password.Verify(input, hashInput);
        
        // assert
        result.Should().Be(true);
    }
    
    [Fact(DisplayName = "Should return false if password is incorrect")]
    [Trait("Domain", "PasswordTest")]
    public void Should_BeEqualTo_False_IfPassword_IsInCorrect()
    {
        // arrange
        var input = "joaozin321";
        var hashInput = "$2a$12$77jHhW..GkrYmNFuVvR.y.XGVxkfAfaKkdCyWaSOrAMRMPOqGbIn6";

        // act
        var result = Password.Verify(input, hashInput);
        
        // assert
        result.Should().Be(false);
    }
}