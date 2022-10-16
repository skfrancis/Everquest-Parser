using System.Text.RegularExpressions;

namespace Utility.Filter.fight;

public class HealFilter : BaseFightFilter
{
    public HealFilter()
    {
        Regexes = new Regex[]
        {
            new(@"^(?<target>.+?) ha(?:s|ve) been healed over time for (?<actual>\d+)(?: \(\d+\))? hit points by(?: (?<spell>.+?))?\.(?: \((?<mod>.+?)\))?$", RegexOptions.Compiled),
            new(@"^(?<source>.+?) healed (?<target>.+?) (?:for|over time for) (?<actual>\d+)(?: \((?<max>\d+)\))? hit points by(?: (?<spell>.+?))?\.(?: \((?<mod>.+?)\))?$", RegexOptions.Compiled),
        };
    }
    
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var target = result.Groups["target"].Value;
        if (target is "himself" or "herself" or "itself") { target = result.Groups["source"].Value; }
        var healAmounts = result.Groups["max"].Success ? $"{result.Groups["actual"].Value}:{result.Groups["max"].Value}" : $"{result.Groups["actual"].Value}:{result.Groups["actual"].Value}";
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], FightTextInfo.ToTitleCase(result.Groups["source"].Value)},
            {Columns[3], FightTextInfo.ToTitleCase(target)},
            {Columns[4], healAmounts},
            {Columns[5], result.Groups["spell"].Value},
            {Columns[6], result.Groups["mod"].Value},
            {Columns[7], "heal"}
        };
        return data;
    }
}