using Utility.Filter.general;
using Utility.Parser;

namespace tests.filter.general;

public class WhoFilterTests
{
    private readonly WhoFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"[1 Druid] Soandso (Halfling)  ZONE: The Mines of Gloomingdeep (tutoriala)", new[] {"Soandso", "Druid", "1"}},
        new object[] {"[60 Oracle (Shaman)] RandomPlayer (Iksar) <Random Guild> ZONE: The Plane of Nightmare (ponightmare)", new[] {"RandomPlayer", "Shaman", "60"}},
        new object[] {"[ANONYMOUS] AnonPlayer", new object[] { "AnonPlayer", "", ""}}
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
        Assert.Equal(results[0], filteredLine?["Name"]);
        Assert.Equal(results[1], filteredLine?["Class"]);
        Assert.Equal(results[2], filteredLine?["Level"]);
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