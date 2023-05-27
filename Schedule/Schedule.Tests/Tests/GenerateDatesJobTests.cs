using Schedule.Application.Jobs;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests;

public sealed class GenerateDatesJobTests
{
    [Theory]
    [MemberData(nameof(IsNeedGenerationCorrectData))]
    public void Test_IsNeedGenerate_ReturnsTrue(DateTime last, DateTime now)
    {
        //Arrange
        var job = TestContainer.Resolve<GenerateDatesJob>();
        
        //Act
        var result = job.IsNeedGenerate(last, now);

        //Assert
        Assert.True(result);
    }
    
    [Theory]
    [MemberData(nameof(IsNeedGenerationIncorrectData))]
    public void Test_IsNeedGenerate_ReturnsFalse(DateTime last, DateTime now)
    {
        //Arrange
        var job = TestContainer.Resolve<GenerateDatesJob>();
        
        //Act
        var result = job.IsNeedGenerate(last, now);

        //Assert
        Assert.False(result);
    }

    public static IEnumerable<object[]> IsNeedGenerationCorrectData =>
        new[]
        {
            new object[]
            {
                new DateTime(2023, 4, 2),
                new DateTime(2023, 4, 9)
            },
            new object[]
            {
                new DateTime(2023, 4, 9),
                new DateTime(2023, 4, 9)
            },
            new object[]
            {
                new DateTime(2022, 12, 31),
                new DateTime(2023, 1, 1)
            },
            new object[]
            {
                new DateTime(2022, 12, 31),
                new DateTime(2023, 1, 2)
            },
            new object[]
            {
                new DateTime(2023, 1, 2),
                new DateTime(2023, 1, 8)
            }
        };
    
    public static IEnumerable<object[]> IsNeedGenerationIncorrectData =>
        new[]
        {
            new object[]
            {
                new DateTime(2023, 4, 10),
                new DateTime(2023, 4, 2)
            },
            new object[]
            {
                new DateTime(2023, 1, 16),
                new DateTime(2023, 1, 2)
            },
            new object[]
            {
                new DateTime(2023, 5, 2),
                new DateTime(2023, 4, 9)
            },
            new object[]
            {
                new DateTime(2023, 1, 2),
                new DateTime(2022, 12, 31)
            },
            new object[]
            {
                new DateTime(2024, 5, 2),
                new DateTime(2023, 4, 9)
            },
        };
}