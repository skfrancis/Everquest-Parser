using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class TradeskillFilter : BaseFilter
{
    public TradeskillFilter()
    {
        Columns.AddRange(new []{"Source", "Created", "Item"});
        Regexes = new Regex[]
        {
            new(@"^(?<source>.+?) (?<created>have fashioned the items together to create [^:]+:) (?<item>[^.]+)\.$", RegexOptions.Compiled),
            new(@"^(?<source>.+?) (?<created>has fashioned) (?<item>[^.]+)\.$", RegexOptions.Compiled),
            new(@"^(?<source>.+?) (?<created>lacked the skills to fashion) (?<item>[^.]+)\.$", RegexOptions.Compiled),
            new(@"^(?<source>.+?) (?<created>was not successful in making) (?<item>[^.]+)\.$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["source"].Value},
            {Columns[3], result.Groups["created"].Value.Contains("fashion").ToString()},
            {Columns[4], result.Groups["item"].Value}
        };
        return data;
    }
}