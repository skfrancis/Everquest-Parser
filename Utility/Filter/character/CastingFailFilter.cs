using System.Text.RegularExpressions;

namespace Utility.Filter.character;

public class CastingFailFilter : BaseFilter
{
    public CastingFailFilter()
    {
        FilterId = "Cast Fail";
        Columns.AddRange(new [] {"Source", "Spell", "Type"});
        Regexes = new Regex[]
        {
            new(@"^(?<source>Your) (?<spell>.+) spell(?: is|) (?<type>\w+)", RegexOptions.Compiled),
            new(@"^(?<source>.*?)'s (?<spell>.+) spell(?: is|) (?<type>\w+)", RegexOptions.Compiled)
        };
    }

    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["source"].Value},
            {Columns[3], result.Groups["spell"].Value},
            {Columns[4], result.Groups["type"].Value},
        };
        return data;    
    }
}