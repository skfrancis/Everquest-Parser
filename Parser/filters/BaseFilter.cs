using Serilog;
using Parser.utility;
using System.Collections;
using System.Text.RegularExpressions;

namespace Parser.filters;

public abstract class BaseFilter
{
    protected List<string> Columns { get; } = new() { "Date", "Time" }; 
    protected IEnumerable<Regex> Regexes { get; init; } = Array.Empty<Regex>();

    public Dictionary<string, string> Filter(ParsedLine logLine)
    {
        foreach (var expression in Regexes)
        {
            var result = expression.Match(logLine.Text);
            if(!result.Success) continue;
            Log.Logger.Debug("Filter Match: [Regex: {Expression}, Text: {Text}", expression.ToString(), logLine.Text);
            return ProcessResult(logLine.Timestamp, result);
        }
        return new Dictionary<string, string>();
    }
    
    protected abstract Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result);
}