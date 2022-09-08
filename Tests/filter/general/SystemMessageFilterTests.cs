
namespace tests.filter.general;

public class SystemMessageFilterTests
{
    private readonly SystemMessageFilter _testFilter = new();

    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"<SYSTEMWIDE_MESSAGE>: Talendor has been defeated by a group of hardy adventurers! Please join us in congratulating Soandso along with everyone else who participated in this achievement!",
            new[] {"Talendor has been defeated by a group of hardy adventurers! Please join us in congratulating Soandso along with everyone else who participated in this achievement!"} },
        new object[] {"<SYSTEMWIDE_MESSAGE>: A proclamation rings out from the royal halls of Felwithe, 'House Thex wishes all across the land to join us in congratulating, Soandso, the first Phantasmist to claim 'Vision' as their own.'", 
            new[] {"A proclamation rings out from the royal halls of Felwithe, 'House Thex wishes all across the land to join us in congratulating, Soandso, the first Phantasmist to claim 'Vision' as their own.'"} }
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
        Assert.Equal(results[0], filteredLine?["Message"]);
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