using Serilog;
using Utility.Filter;
using Utility.Parser;

var filePath = args[0];
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var tokenSource = new CancellationTokenSource();
var filterHandler = new FilterHandler();
var fileHandler = new LogFileHandler(filePath, LogHandler, tokenSource.Token);

try
{
    await fileHandler.Start();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

void LogHandler(ParsedLineObject parsedLine)
{
    var result = FilterHandler.ProcessFilters(parsedLine);
    if (result != null)
    {
        Log.Logger.Information("TimeStamp: {Timestamp} Text: {Text} FilterId: {FilterId}", parsedLine.Timestamp, parsedLine.Text, result["FilterId"]);
    }
    else
    {
        Log.Logger.Information("TimeStamp: {Timestamp} Text: {Text}", parsedLine.Timestamp, parsedLine.Text);
    }
}