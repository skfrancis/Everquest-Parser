using Utility.Filter.character;
using Utility.Parser;

namespace Tests.filter.character;

public class AchievementFilterTests
{
    private readonly AchievementFilter _testFilter = new();
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] { "Your guildmate Soandso has completed Guild Lobby Traveler achievement.", new[] {"Soandso", "Guild Lobby Traveler"} },
        new object[] { "You have completed Guild Lobby Traveler achievement.", new[] {"You", "Guild Lobby Traveler"} }
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ValidationTests(string text, string [] results)
    {
        var timeStamp = DateTime.Now;
        var parsedLine = new ParsedLineObject(timeStamp, text);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(timeStamp.ToShortDateString(), filteredLine?["Date"]);
        Assert.Equal(timeStamp.TimeOfDay.ToString(), filteredLine?["Time"]);
        Assert.Equal(results[0], filteredLine?["Player"]);
        Assert.Equal(results[1], filteredLine?["Achievement"]);
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