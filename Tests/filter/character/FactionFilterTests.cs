namespace Tests.filter.character;

public class FactionFilterTests
{
    private readonly FactionFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Your faction standing with Minions of Tirranun has been adjusted by -1.", new[] {"Minions of Tirranun", "-1"} },
        new object[] {"Your faction standing with Overlord Mata Muram has been adjusted by 1.", new[] {"Overlord Mata Muram", "1"} },
        new object[] {"Your faction standing with Children of Dranik could not possibly get any better.", new[] {"Children of Dranik", "max"}},
        new object[] {"Your faction standing with Trakanon could not possibly get any worse.", new[] {"Trakanon", "min"}},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ValidationTests(string text, string[] results)
    {
        var timeStamp = DateTime.Now;
        var parsedLine = new ParsedLineObject(timeStamp, text);
        var filteredLine = _testFilter.Filter(parsedLine);
        Assert.Equal(timeStamp.ToShortDateString(), filteredLine["Date"]);
        Assert.Equal(timeStamp.TimeOfDay.ToString(), filteredLine["Time"]);
        Assert.Equal(results[0], filteredLine["Faction"]);
        Assert.Equal(results[1], filteredLine["Amount"]);
        
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