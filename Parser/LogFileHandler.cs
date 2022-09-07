using System.Text;
using Serilog;

namespace Parser;

public class LogFileHandler
{
    private readonly string _filePath;
    private readonly CancellationToken _token;
    private readonly Action<ParsedLineObject> _handler;
    private FileStream? _logFileStream;
    private StreamReader? _logFileStreamReader;

    public LogFileHandler(string filePath, Action<ParsedLineObject> handler, CancellationToken token)
    {
        _filePath = filePath;
        _handler = handler;
        _token = token;
    }

    public Task Start()
    {
        Log.Logger.Debug("Log File: {FilePath}", _filePath);
        try
        {
            _logFileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _logFileStreamReader = new StreamReader(_logFileStream, Encoding.UTF8);
            _logFileStream.Seek(0, SeekOrigin.End);
            return Task.Factory.StartNew(Run, TaskCreationOptions.LongRunning);
        }
        catch (Exception e)
        {
            Log.Logger.Error("Start failed: {Message}", e.Message);
            return Task.FromException(e);
        }
    }

    private void Run()
    {
        while (!_token.IsCancellationRequested)
        {
            var logLine = _logFileStreamReader?.ReadLine();
            if (logLine != null)
            {
                Log.Debug("Log line: {LogLine}", logLine);
                var parsedLine = LineParser.Parse(logLine);
                _handler(parsedLine);
            }
            else
            {
                Thread.Sleep(100);
            }
        }
        _logFileStreamReader?.Close();
        _logFileStream?.Close();
    }
}