namespace Tests.filter.character;

public class SkillsFilterTests
{
    private readonly SkillsFilter _testFilter = new();
    
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"You have become better at 1H Blunt! (198)", new[] {"1H Blunt", "198"} },
        new object[] {"You have become better at Alteration! (108)", new[] {"Alteration", "108"} },
        new object[] {"You have become better at Offense! (6)", new[] {"Offense", "6"} },
        new object[] {"You have become better at 1H Piercing! (40)", new[] {"1H Piercing", "40"} }
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
        Assert.Equal(results[0], filteredLine?["Skill"]);
        Assert.Equal(results[1], filteredLine?["Level"]);
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