using Filter.character;
using Parser;

namespace Tests.filter.character;

public class AchievementFilterTests
{
    private readonly AchievementFilter _testFilter = new();
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] { "Your guildmate Soandso has completed Guild Lobby Traveler achievement.", "Soandso", "Guild Lobby Traveler" },
        new object[] { "You have completed Guild Lobby Traveler achievement.", "You", "Guild Lobby Traveler" }
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ValidationTests(string text, string player, string achievement)
    {
        var timeStamp = DateTime.Now;
        var parsedLine = new ParsedLine(timeStamp, text);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(timeStamp.ToShortDateString(), filteredLine["Date"]);
        Assert.Equal(timeStamp.TimeOfDay.ToString(), filteredLine["Time"]);
        Assert.Equal(player, filteredLine["Player"]);
        Assert.Equal(achievement, filteredLine["Achievement"]);

    }
    
    [Fact]
    public void UnfilteredLine()
    {
        const string validLine = "[Thu Jul 07 10:59:01 2022] Welcome to EverQuest!";
        var parsedLine = LineParser.Parse(validLine);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(new Dictionary<string, string>(), filteredLine);
    }
}