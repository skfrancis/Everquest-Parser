using Parser.utility;

namespace ParserTesting.utility;

public class LineParserTests
{
    [Fact]
    public void LogLineValid()
    {
        const string validLine = "[Thu Jul 07 10:59:01 2022] Welcome to EverQuest!";
        var parsedLine = LineParser.Parse(validLine);
        Assert.Equal(new DateTime(2022, 07, 07, 10, 59, 01), parsedLine.Timestamp);
        Assert.Equal("Welcome to EverQuest!", parsedLine.Text);
    }

    [Fact]
    public void LogLineInvalid()
    {
        const string emptyLine = "This is an invalid log line";
        var parsedLine = LineParser.Parse(emptyLine);
        Assert.Equal(DateTime.MinValue, parsedLine.Timestamp);
        Assert.Equal(string.Empty, parsedLine.Text);
    }

    [Fact]
    public void LogLineEmpty()
    {
        const string emptyLine = "";
        var parsedLine = LineParser.Parse(emptyLine);
        Assert.Equal(DateTime.MinValue, parsedLine.Timestamp);
        Assert.Equal(string.Empty, parsedLine.Text);
    }
}