using Utility.Filter;
using Utility.Parser;

namespace tests.filter;

public class FilterHandlerTests
{
    private readonly FilterHandler _filterHandler = new ();
    
    [Fact]
    public void UnfilteredLine()
    {
        const string validLine = "[Thu Jul 07 10:59:01 2022] Welcome to EverQuest!";
        var parsedLine = LineParser.Parse(validLine);
        var filteredLine = _filterHandler.ProcessFilters(parsedLine);
        Assert.Null(filteredLine);
    }
}