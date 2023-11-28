using Application.UnitTest.Fixture;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Application.Users.LoginUser;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.User;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Application.UnitTest.Users;

[Collection(nameof(CollectionApplicationFixture))]
public class LoginUserTest
{
    private readonly ApplicationFixture _fixture;

    public LoginUserTest(ApplicationFixture fixture)
        => (_fixture) = (fixture);

    [Fact(DisplayName = "Given the wrong password should not create the token")]
    [Trait("Application", "Users")]
    public async Task Should_NotCreate_TokenIf_GivenPassword_IsWrong()
    {
        // arrange
        var mocker = new AutoMocker();
        var query = new LoginUserQuery("attest@gmail.com", "testpasswor00..");
        var user = _fixture.GenerateUser("user test", query.Email, "testpassword0.");
        var loginHandler = mocker.CreateInstance<LoginUserQueryHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns(Task.FromResult(user));
        
        // act
        var result = await loginHandler.Handle(query, default);
        
        // assert
        result.Value.Should().BeOfType<Error>();
    }
    
    [Fact(DisplayName = "If user not found should return not found code")]
    [Trait("Application", "Users")]
    public async Task Should_ReturnNotFound_IfUserDoesntExists()
    {
        // arrange
        var mocker = new AutoMocker();
        var query = new LoginUserQuery("attest@gmail.com", "testpasswor00..");
        var loginHandler = mocker.CreateInstance<LoginUserQueryHandler>();
        
        // act
        var result = await loginHandler.Handle(query, default);
        
        // assert
        result.Value.Should().BeOfType<Error>();
        var value = result.Match(
            ok => "",
            error => error.Code);
        value.Should().Be("Not Found");
    }
    
    [Fact(DisplayName = "Should create the token if user input are correct")]
    [Trait("Application", "Users")]
    public async Task Should_ReturnUserToken_IfInputsAreCorrect()
    {
        // arrange
        var mocker = new AutoMocker();
        var query = new LoginUserQuery("attest@gmail.com", "testpasswor00..");
        var user = _fixture.GenerateUser("user test", query.Email, query.Password);
        var loginHandler = mocker.CreateInstance<LoginUserQueryHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByEmail(It.IsAny<string>()))
            .Returns(Task.FromResult(user));
        mocker.GetMock<IConfiguration>()
            .SetupGet(c => c[It.Is<string>(s => s == "Jwt:Key")])
            .Returns("2a50d9b8f9c4377a86fd8494b2f80b3c5e489740b06d27b56a7f92e924e42bf3");
        mocker.GetMock<IConfiguration>()
            .SetupGet(c => c[It.Is<string>(s => s == "Jwt:Issuer")])
            .Returns("c2VuaGFzdXBlcnNlY3JldGFpc3N1ZXI=s");
        mocker.GetMock<IConfiguration>()
            .SetupGet(c => c[It.Is<string>(s => s == "Jwt:Audience")])
            .Returns("c2VuaGFzdXBlcnNlY3JldGFhdWRpZW5jZQo=");
        
        // act
        var result = await loginHandler.Handle(query, default);
        
        // assert
        result.Value.Should().BeOfType<ApiResponse<UserLoggedDto>>();
        var value = result.Match(
            ok => ok,
            error => null);
        value.Data.Token.Should().NotBeNull();
        value.Message.Should().Be("OK");
        value.Success.Should().Be(true);
    }
}