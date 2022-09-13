using Parser;
using Serilog;

var filePath = args[0];
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var tokenSource = new CancellationTokenSource();
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
    Log.Logger.Information("TimeStamp: {Timestamp} Text: {Text}", parsedLine.Timestamp, parsedLine.Text);
}
