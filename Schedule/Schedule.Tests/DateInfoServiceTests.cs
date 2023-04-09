using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Tests.Common;

namespace Schedule.Tests;

public sealed class DateInfoServiceTests
{
    [Theory]
    [MemberData(nameof(IsGetWeekOfYearData))]
    public void Test_GetWeekOfYear_ReturnsCorrectValue(DateTime dateTime, int expected)
    {
        //Arrange
        var service = TestContainer.Resolve<IDateInfoService>();
        
        //Act
        var result = service.GetWeekOfYear(dateTime);

        //Assert
        Assert.Equal(expected, result);
    }
    
    public static IEnumerable<object[]> IsGetWeekOfYearData =>
        new[]
        {
            new object[] { new DateTime(2023, 1, 2), 1 },
            new object[] { new DateTime(2023, 4, 2), 13 },
            new object[] { new DateTime(2023, 1, 1), 52 },
            new object[] { new DateTime(2022, 12, 31), 52 },
            new object[] { new DateTime(2025, 9, 5), 36 }
        };
    
    [Theory]
    [MemberData(nameof(IsGetWeekTypeData))]
    public void Test_GetWeekType_ReturnsCorrectValue(DateTime dateTime, WeekType expected)
    {
        //Arrange
        var service = TestContainer.Resolve<IDateInfoService>();
        
        //Act
        var result = service.GetWeekType(dateTime);

        //Assert
        Assert.Equal(expected, result);
    }
    
    public static IEnumerable<object[]> IsGetWeekTypeData =>
        new[]
        {
            new object[] { new DateTime(2023, 1, 2), WeekType.Green },
            new object[] { new DateTime(2023, 4, 2), WeekType.Green },
            new object[] { new DateTime(2023, 1, 1), WeekType.Yellow },
            new object[] { new DateTime(2022, 12, 31), WeekType.Yellow },
            new object[] { new DateTime(2025, 9, 5), WeekType.Yellow }
        };
}