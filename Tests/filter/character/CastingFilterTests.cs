
namespace Tests.filter.character;

public class CastingFilterTests
{
    private readonly CastingFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"Soandso begins casting Talisman of the Beast.", new[] {"Soandso", "Talisman of the Beast"} },
        new object[] {"Soandso begins singing Selo's Accelerando.", new[] {"Soandso", "Selo's Accelerando"} },
        new object[] {"Soandso activates Protective Spirit Discipline.", new[] {"Soandso", "Protective Spirit Discipline"} },
        new object[] {"You begin casting Spiritual Radiance.", new[] {"You", "Spiritual Radiance"} },
        new object[] {"You begin singing Hymn of Restoration.", new[] {"You", "Hymn of Restoration"} },
        new object[] {"You activate Fearless Discipline.", new[] {"You", "Fearless Discipline"}}
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
        Assert.Equal(results[0], filteredLine["Source"]);
        Assert.Equal(results[1], filteredLine["Spell"]);
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