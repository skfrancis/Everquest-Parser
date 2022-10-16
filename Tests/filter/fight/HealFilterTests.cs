using Utility.Filter.fight;
using Utility.Parser;

namespace tests.filter.fight;

public class HealFilterTests
{
    private readonly HealFilter _testFilter = new();
    
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Soandso healed Randomplayer for 2257 (3024) hit points by Ethereal Light.", new[] {"Soandso", "Randomplayer", "2257:3024", "Ethereal Light", "", "heal" }},
        new object[] {"Soandso healed Randomplayer for 1304 (5628) hit points by Ethereal Light. (Critical)", new[] {"Soandso", "Randomplayer", "1304:5628", "Ethereal Light", "Critical", "heal" }},
        new object[] {"Soandso healed herself over time for 738 hit points by Elixir of Healing X.", new[] {"Soandso", "Soandso", "738:738", "Elixir of Healing X", "", "heal" }},
        new object[] {"Soandso has been healed over time for 250 hit points by Celestial Regeneration I.", new[] {"", "Soandso", "250:250", "Celestial Regeneration I", "", "heal" }},
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
        Assert.Equal(results[0], filteredLine?["Source"]);
        Assert.Equal(results[1], filteredLine?["Target"]);
        Assert.Equal(results[2], filteredLine?["Amount"]);
        Assert.Equal(results[3], filteredLine?["Ability"]);
        Assert.Equal(results[4], filteredLine?["Mod"]);
        Assert.Equal(results[5], filteredLine?["Type"]);
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