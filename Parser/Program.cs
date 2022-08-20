using Parser.utility;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

const string filePath = "D:\\Games\\Steam\\steamapps\\common\\Everquest F2P\\Logs\\eqlog_Rarshaak_vaniki.txt";
var tokenSource = new CancellationTokenSource();

void LogHandler(ParsedLine parsedLine)
{
    Console.WriteLine(parsedLine);
}

var fileHandler = new LogFileHandler(filePath, LogHandler, tokenSource.Token);
await fileHandler.Start();