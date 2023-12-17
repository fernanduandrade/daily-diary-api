using Application.UnitTest.Fixture;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Application.Users.GetUserById;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace Application.UnitTest.Users;

[Collection(nameof(CollectionApplicationFixture))]
public class GetUserByIdTest
{
    private readonly ApplicationFixture _fixture;

    public GetUserByIdTest(ApplicationFixture fixture)
        => (_fixture) = (fixture);

    [Fact(DisplayName = "Should return not found if user doesn't exists")]
    [Trait("Application ", "User")]
    public async Task Should_ReturnNotFound_IfUser_DoesntExists()
    {
        // arrange
        var id = _fixture.GetRandomGuid();
        var query = new GetUserByIdQuery(id);
        var mocker = new AutoMocker();
        var getByIdHandler = mocker.CreateInstance<GetUserByIdQueryHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByIdAsync(query.Id)).Returns(Task.FromResult<User>(null));
        // act
        var result = await getByIdHandler.Handle(query, default);
        
        // assert
        result.Value.Should().BeOfType<Error>();
    }
    
    [Fact(DisplayName = "Should return user if exists")]
    [Trait("Application ", "User")]
    public async Task Should_ReturnUser_If_Exists()
    {
        // arrange
        var id = _fixture.GetRandomGuid();
        var query = new GetUserByIdQuery(id);
        var mocker = new AutoMocker();
        var getByIdHandler = mocker.CreateInstance<GetUserByIdQueryHandler>();
        mocker.GetMock<IUserRepository>()
            .Setup(x => x
                .GetByIdAsync(query.Id))
            .Returns(Task.FromResult<User>(_fixture
                .GenerateUser("test", "test@agotum.com", "123")));
        // act
        var result = await getByIdHandler.Handle(query, default);
        
        // assert
        result.Value.Should().BeOfType<ApiResponse<UserDto>>();
    }
}