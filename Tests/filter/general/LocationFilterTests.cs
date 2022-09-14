using Utility.Filter.general;
using Utility.Parser;

namespace tests.filter.general;

public class LocationFilterTests
{
    private readonly LocationFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Your Location is 43.22, 11.70, 3.74", new[] {"43.22", "11.70", "3.74"} },
        new object[] {"Your Location is -431.22, 111.20, -223.65", new[] {"-431.22", "111.20", "-223.65"} }
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
        Assert.Equal(results[0], filteredLine?["Y"]);
        Assert.Equal(results[1], filteredLine?["X"]);
        Assert.Equal(results[2], filteredLine?["Z"]);
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