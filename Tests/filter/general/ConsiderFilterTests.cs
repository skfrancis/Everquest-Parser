using Utility.Filter.general;
using Utility.Parser;

namespace tests.filter.general;

public class ConsiderFilterTests
{
    private readonly ConsiderFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Granitesmash - a rare creature - scowls at you, ready to attack -- what would you like your tombstone to say? (Lvl: 67)", new [] {"Granitesmash", "True", "scowls at you, ready to attack", "what would you like your tombstone to say?", "67"} },
        new object[] {"Balance of Speed glares at you threateningly -- what would you like your tombstone to say? (Lvl: 74)", new [] {"Balance of Speed", "False", "glares at you threateningly", "what would you like your tombstone to say?", "74"} },
        new object[] {"Qrixat Du`voy glowers at you dubiously -- looks like she would wipe the floor with you! (Lvl: 61)", new [] {"Qrixat Du`voy", "False", "glowers at you dubiously", "looks like she would wipe the floor with you!", "61"} },
        new object[] {"Nagask regards you indifferently -- looks like quite a gamble. (Lvl: 60)", new [] {"Nagask", "False", "regards you indifferently", "looks like quite a gamble.", "60"} },
        new object[] {"Soandso`s warder judges you amiably -- looks kind of dangerous. (Lvl: 48)", new [] {"Soandso`s warder", "False", "judges you amiably", "looks kind of dangerous.", "48"} }
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
        Assert.Equal(results[0], filteredLine?["Target"]);
        Assert.Equal(results[1], filteredLine?["Rare"]);
        Assert.Equal(results[2], filteredLine?["Consider"]);
        Assert.Equal(results[3], filteredLine?["Difficulty"]);
        Assert.Equal(results[4], filteredLine?["Level"]);
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