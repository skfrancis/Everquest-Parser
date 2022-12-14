using System.Text.RegularExpressions;
using Serilog;
using Utility.Parser;

namespace Utility.Filter;

public abstract class BaseFilter
{
    protected string FilterId = string.Empty;
    protected List<string> Columns { get; } = new() { "Date", "Time" }; 
    protected IEnumerable<Regex> Regexes { get; init; } = Array.Empty<Regex>();

    public Dictionary<string, string>? Filter(ParsedLineObject logLine)
    {
        foreach (var expression in Regexes)
        {
            var result = expression.Match(logLine.Text);
            if(!result.Success) continue;
            Log.Logger.Debug("Filter Match: [FilterId: {FilterId}, Text: {Text}", FilterId, logLine.Text);
            return ProcessResult(logLine.Timestamp, result);
        }
        return null;
    }
    
    protected abstract Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result);
}