using DailyDiary.Domain.Diaries;
using FluentAssertions;

namespace Domain.UniTest.Diaries;

public class DiaryTest
{

    [Fact(DisplayName = "Should create a diary private")]
    [Trait("Domain", "DiaryTest")]
    public void Should_Create_PrivateDiary()
    {
        // arrange
        
        // act
        var diary = Diary.Create("test", "something", "sad", Guid.NewGuid());
        
        // assert
        diary.IsPublic.Should().Be(false);
    }
    
    [Fact(DisplayName = "Should create a diary private")]
    [Trait("Domain", "DiaryTest")]
    public void Should_Create_PublicDiary()
    {
        // arrange
        
        // act
        var diary = Diary.Create("test", "something", "sad", Guid.NewGuid(), true);
        
        // assert
        diary.IsPublic.Should().Be(true);
        diary.Title.Should().Be("test");
        diary.CreatedAt.Should().BeAfter(DateTime.Now);
        diary.Text.Should().Be("something");
    }
}