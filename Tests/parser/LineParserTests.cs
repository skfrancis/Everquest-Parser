using Utility.Parser;

namespace Tests.parser;

public class LineParserTests
{
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] { "[Thu Jul 07 10:59:01 2022] Welcome to EverQuest!", new DateTime(2022, 07, 07, 10, 59, 01), "Welcome to EverQuest!" },
        new object[] { "[Thu Jul 99 10:59:01 2022] Welcome to EverQuest!", DateTime.MinValue, string.Empty },
        new object[] { "This is an invalid log line", DateTime.MinValue, string.Empty },
        new object[] { "", DateTime.MinValue, string.Empty },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ValidationTests(string logLine, DateTime timestamp, string text)
    {
        var parsedLine = LineParser.Parse(logLine);
        Assert.Equal(timestamp, parsedLine.Timestamp);
        Assert.Equal(text, parsedLine.Text);
    }        
}