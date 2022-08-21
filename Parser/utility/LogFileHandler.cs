using Serilog;
using System.Text;

namespace Parser.utility;

public class LogFileHandler
{
    private readonly CancellationToken _token;
    private readonly Action<ParsedLine> _handler;
    private readonly FileStream? _logFileStream;
    private readonly StreamReader? _logFileStreamReader;


    public LogFileHandler(string filePath, Action<ParsedLine> handler, CancellationToken token)
    {
        _token = token;
        _handler = handler;
        if (File.Exists(filePath))
        {
            Log.Logger.Debug("Log File: {FilePath}", filePath);
            _logFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _logFileStreamReader = new StreamReader(_logFileStream, Encoding.UTF8);
        }
        else
        {
            Log.Logger.Error("File not found: {FilePath}", filePath);
        }
    }

    public Task Start()
    {
        _logFileStream?.Seek(0, SeekOrigin.End);
        return Task.Factory.StartNew(Run, TaskCreationOptions.LongRunning);
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