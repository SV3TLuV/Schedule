using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests;

public sealed class DateInfoServiceTests
{
    public static IEnumerable<object[]> IsGetWeekOfYearData =>
        new[]
        {
            new object[] { new DateTime(2023, 1, 2), 1 },
            new object[] { new DateTime(2023, 4, 2), 13 },
            new object[] { new DateTime(2023, 1, 1), 52 },
            new object[] { new DateTime(2022, 12, 31), 52 },
            new object[] { new DateTime(2025, 9, 5), 36 }
        };

    public static IEnumerable<object[]> IsGetWeekTypeData =>
        new[]
        {
            new object[] { new DateTime(2023, 9, 1), WeekType.Yellow },
            new object[] { new DateTime(2022, 9, 1), WeekType.Yellow },
            new object[] { new DateTime(2021, 9, 1), WeekType.Yellow },
            new object[] { new DateTime(2023, 4, 10), WeekType.Green },
            new object[] { new DateTime(2023, 4, 9), WeekType.Yellow },
            new object[] { new DateTime(2023, 1, 12), WeekType.Yellow },
            new object[] { new DateTime(2022, 1, 12), WeekType.Yellow },
            new object[] { new DateTime(2021, 1, 12), WeekType.Yellow }
        };

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
}