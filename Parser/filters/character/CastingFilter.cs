using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class CastingFilter : BaseFilter
{
    public CastingFilter()
    {
        FilterId = "Casting";
        Columns.AddRange(new [] {"Source", "Spell"});
        Regexes = new Regex[]
        {
            new(@"^(?<source>.+?) begins? (?:casting|singing) (?<spell>.+)\.$", RegexOptions.Compiled),
            new(@"^(?<source>.+?) activates? (?<spell>.+)\.$", RegexOptions.Compiled)
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
            {Columns[3], result.Groups["spell"].Value}
        };
        return data;
    }
}