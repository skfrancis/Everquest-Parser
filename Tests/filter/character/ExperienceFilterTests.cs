namespace Tests.filter.character;

public class ExperienceFilterTests
{
    private readonly ExperienceFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"You gain experience!", new[] {"solo", "False"} },
        new object[] {"You gain party experience!", new[] {"party", "False"} },
        new object[] {"You gained raid experience!", new[] {"raid", "False"} },
        new object[] {"You gain experience (with a bonus)!", new[] {"solo", "True"} },
        new object[] {"You gain party experience (with a bonus)!", new[] {"party", "True"} },
        new object[] {"You gained raid experience (with a bonus)!", new[] {"raid", "True"} }
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ValidationTests(string text, string[] results)
    {
        var timeStamp = DateTime.Now;
        var parsedLine = new ParsedLineObject(timeStamp, text);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(timeStamp.ToShortDateString(), filteredLine?["Date"]);
        Assert.Equal(timeStamp.TimeOfDay.ToString(), filteredLine?["Time"]);
        Assert.Equal(results[0], filteredLine?["Type"]);
        Assert.Equal(results[1], filteredLine?["Bonus"]);
    }

    [Fact]
    public void UnfilteredLine()
    {
        const string validLine = "[Thu Jul 07 10:59:01 2022] Welcome to EverQuest!";
        var parsedLine = LineParser.Parse(validLine);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Null(filteredLine);
    }
}