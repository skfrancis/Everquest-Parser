
namespace Tests.filter.character;

public class AltAdvancementFilterTests
{
    private readonly AltAdvancementFilter _testFilter = new();
    
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] { "You have gained an ability point!  You now have 1 ability point.", new[] {"1", "1"} },
        new object[] { "You have gained an ability point!  You now have 6 ability points.", new[] {"1", "6"} },
        new object[] { "You have gained an ability point!  You now have 60 ability points.", new[] {"1", "60"} },
        new object[] { "You have gained 1 ability point(s)!  You now have 3 ability point(s).", new[] {"1", "3"} },
        new object[] { "You have gained 2 ability point(s)!  You now have 27 ability point(s).", new[] {"2", "27"} },
        new object[] { "You have gained 15 ability point(s)!  You now have 65 ability point(s).", new[] {"15", "65"} }
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
        Assert.Equal(results[0], filteredLine?["Gained"]);
        Assert.Equal(results[1], filteredLine?["Banked"]);
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