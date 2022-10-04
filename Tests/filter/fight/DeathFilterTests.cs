using Utility.Filter.fight;
using Utility.Parser;

namespace tests.filter.fight;

public class DeathFilterTests
{
    private readonly DeathFilter _testFilter = new();
    
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Soandso has been slain by a Bolvirk warrior!", new[] {"A Bolvirk Warrior", "Soandso", "", "", "", "death" }},
        new object[] {"You have been slain by an ukun juxtapincer!", new[] {"An Ukun Juxtapincer", "You", "", "", "", "death" }},
        new object[] {"A Bolvirk defender has been slain by Soandso!", new[] {"Soandso", "A Bolvirk Defender", "", "", "", "death" }},
        new object[] {"You have slain a lightning warrior diode!", new[] {"You", "A Lightning Warrior Diode", "", "", "", "death" }},
        new object[] {"Soandso dies.", new[] {"Soandso", "Soandso", "", "", "", "death" }},
        new object[] {"Balance of Speed died.", new[] {"Balance Of Speed", "Balance Of Speed", "", "", "", "death" }}
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
        Assert.Equal(results[2], filteredLine?["Damage"]);
        Assert.Equal(results[3], filteredLine?["Amount"]);
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