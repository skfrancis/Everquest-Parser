using Utility.Filter.character;
using Utility.Parser;

namespace Tests.filter.character;

public class CastingFailFilterTests
{
    private readonly CastingFailFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Your Flash of Light spell fizzles!", new[] {"Your", "Flash of Light", "fizzles"} },
        new object[] {"Your Flash of Light spell is interrupted.", new[] {"Your", "Flash of Light", "interrupted"} },
        new object[] {"Soandso's Pack Chloroplast spell fizzles!", new[] {"Soandso", "Pack Chloroplast", "fizzles"} },
        new object[] {"Soandso's Turgur's Insects spell fizzles!", new[] {"Soandso", "Turgur's Insects", "fizzles"} },
        new object[] {"Soandso's Pack Chloroplast spell is interrupted.", new[] {"Soandso", "Pack Chloroplast", "interrupted"} },
        new object[] {"Soandso's Turgur's Insects spell is interrupted.", new[] {"Soandso", "Turgur's Insects", "interrupted"} }
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
        Assert.Equal(results[1], filteredLine?["Spell"]);
        Assert.Equal(results[2], filteredLine?["Type"]);
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