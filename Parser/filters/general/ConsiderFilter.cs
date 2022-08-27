using System.Text.RegularExpressions;

namespace Parser.filters.general;

public class ConsiderFilter : BaseFilter
{
    public ConsiderFilter()
    {
        FilterId = "Consider";
        Columns.AddRange(new [] {"Target", "Rare", "Consider", "Difficulty", "Level"});
        Regexes = new Regex[]
        {
            new(@"(?<target>.+\b) (?<rare>- .+)?(?<consider>(?:scowls|glares|glowers|regards|looks|judges|kindly) .+?) -- (?<difficulty>.+) \(Lvl: (?<level>\d+)\)$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["target"].Value},
            {Columns[3], result.Groups["rare"].Success.ToString()},
            {Columns[4], result.Groups["consider"].Value},
            {Columns[5], result.Groups["difficulty"].Value},
            {Columns[6], result.Groups["level"].Value}
        };
        return data;
    }
}