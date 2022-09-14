using Utility.Filter.character;
using Utility.Parser;

namespace Tests.filter.character;

public class PetLeaderFilterTests
{
    private readonly PetLeaderFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Zebektik says, 'My leader is Soandso.'", new[] {"Soandso", "Zebektik"} },
        new object[] {"Soandso`s warder says, 'My leader is Soandso.'", new[] {"Soandso", "Soandso`s warder"} }
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
        Assert.Equal(results[0], filteredLine?["Leader"]);
        Assert.Equal(results[1], filteredLine?["Pet"]);
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