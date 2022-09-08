namespace Tests.filter.character;

public class TradeskillFilterTests
{
    private readonly TradeskillFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"You have fashioned the items together to create something new: Silk Swatch.", new[] {"You", "True", "Silk Swatch"} },
        new object[] {"You have fashioned the items together to create an alternate product: Bottle.", new[] {"You", "True", "Bottle"} },
        new object[] {"Soandso has fashioned Locked Parts Box.", new[] {"Soandso", "True", "Locked Parts Box"} },
        new object[] {"You lacked the skills to fashion Woven Mandrake.", new[] {"You", "False", "Woven Mandrake"} },
        new object[] {"Soandso was not successful in making Forged Javelin.", new[] {"Soandso", "False", "Forged Javelin"} },
        new object[] {"Soandso was not successful in making Distillate of Spirituality IX, but made Small Vial as an alternate product!", new[] {"Soandso", "False", "Distillate of Spirituality IX"}}
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
        Assert.Equal(results[1], filteredLine["Created"]);
        Assert.Equal(results[2], filteredLine["Item"]);
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