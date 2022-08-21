using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class SkillsFilter : BaseFilter
{
    public SkillsFilter()
    {
        Columns.AddRange(new [] {"Skill", "Level"});
        Regexes = new Regex[]
        {
            new(@"^You have become better at (?<skill>.+)! \((?<level>\d+)\)$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["skill"].Value},
            {Columns[3], result.Groups["level"].Value}
        };
        return data;
    }
}