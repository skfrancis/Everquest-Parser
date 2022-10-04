using Utility.Filter.general;
using Utility.Parser;

namespace tests.filter.general;

public class ZoneFilterTests
{
    private readonly ZoneFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"You have entered The Devastation.", new[] {"The Devastation"} },
        new object[] {"You have entered East Freeport.", new[] {"East Freeport"} },
        new object[] {"You have entered Sverag, Stronghold of Rage.", new[] {"Sverag, Stronghold of Rage"} }
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
        Assert.Equal(results[0], filteredLine?["Zone"]);
    }

    public static IEnumerable<object[]> IgnoredData => new List<object[]>
    {
        new object[] {"You have entered an area where levitation effects do not function.", new Dictionary<string, string>() },
        new object[] {"You have entered an Arena (PvP) area.", new Dictionary<string, string>() },
        new object[] {"You have entered an area where Bind Affinity is allowed.", new Dictionary<string, string>() }
    };

    [Theory]
    [MemberData(nameof(IgnoredData))]
    public void IgnoredValidationTests(string text, Dictionary<string, string> result)
    {
        var timeStamp = DateTime.Now;
        var parsedLine = new ParsedLineObject(timeStamp, text);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(result, filteredLine);
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