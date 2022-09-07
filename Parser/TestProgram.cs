using Parser;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

//const string filePath = "D:\\Games\\Steam\\steamapps\\common\\Everquest F2P\\Logs\\eqlog_Rarshaak_vaniki.txt";
const string filePath = "eqlog_Rarshaak_vaniki.txt";
var tokenSource = new CancellationTokenSource();

void LogHandler(ParsedLineObject parsedLine)
{
    Console.WriteLine("Data: {0}", parsedLine);
}

var fileHandler = new LogFileHandler(filePath, LogHandler, tokenSource.Token);

try
{
    await fileHandler.Start();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
