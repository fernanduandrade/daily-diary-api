using Application.UnitTest.Fixture;
using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Shared.Dto;
using DailyDiary.Application.Users.CreateUser;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Application.UnitTest.Users;

[Collection(nameof(CollectionApplicationFixture))]
public class CreateUserTest
{
    private readonly ApplicationFixture _fixture;
    
    public CreateUserTest(ApplicationFixture fixture)
        => (_fixture) = (fixture);

    [Fact(DisplayName = "Should not create user when email already exists")]
    [Trait("Application", "User")]
    public async Task Should_Not_CreateUser_When_EmailExists()
    {
        // arrange
        var command = new CreateUserCommand("Alicia", "alicia@tester.com", "teste123") ;
        var mocker = new AutoMocker();
        var user = _fixture.GenerateUser(command.Name, command.Email, command.Password);
        var createUserHandler = mocker.CreateInstance<CreateUserCommandHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByEmail(command.Email)).Returns(Task.FromResult(user));
        
        // act
        var result = await createUserHandler.Handle(command, default);
        
        // assert
        result.Value.Should().BeOfType<Error>();
    }
    
    [Fact(DisplayName = "Should create user when payload is valid")]
    [Trait("Application", "User")]
    public async Task Should_CreateUser_When_FieldsAreValid()
    {
        // arrange
        var command = new CreateUserCommand("Alicia", "alicia@tester.com", "teste123") ;
        var user = _fixture.GenerateUser(command.Name, command.Email, command.Password);
        var mocker = new AutoMocker();
        var createUserHandler = mocker.CreateInstance<CreateUserCommandHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByEmail(command.Email)).Returns(Task.FromResult(It.IsAny<User>()));
        mocker.GetMock<IUserRepository>()
            .Setup(x => x
                .AddAsync(user)).Returns(Task.FromResult(user));
        mocker.GetMock<IMapper>()
            .Setup(x => x.Map<UserDto>(It.IsAny<object>())).Returns(_fixture.CreateUserDto(user));
        
        // act
        var result = await createUserHandler.Handle(command, default);
        
        // assert
        result.Value.Should().BeOfType<ApiResponse<UserDto>>();
        var userResponse = result.Match(
            ok => ok,
            _ => null);
        userResponse.Should().NotBeNull();
        userResponse.Data.Email.Should().Be(command.Email);
        userResponse.Data.Name.Should().Be(command.Name);
    }
}