using System.Text.RegularExpressions;

namespace Filter.fight;

public class MeleeFilter : BaseFightFilter
{
    public MeleeFilter()
    {
        Regexes = new Regex[]
        {
            new(@"^(?<source>.+?) (?<damage>slash|bash|hit|crush|punch|frenz|pierce|claw|kick|bite|backstab|strike|maul|gore|slice|smash|sting|rend|slam|shoot|stab|burn|learn|sweep)(es|s|y on|ies on|) (?<target>.+) for (?<amount>\d+) points? of damage\.(?: \((?<modifier>.+)\))?$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["source"].Value},
            {Columns[3], result.Groups["target"].Value},
            {Columns[4], result.Groups["damage"].Value},
            {Columns[5], result.Groups["amount"].Value},
            {Columns[6], result.Groups["modifier"].Value},
            {Columns[7], "melee"}
        };
        return data;
    }
}