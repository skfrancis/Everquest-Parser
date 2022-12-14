using System.Text.RegularExpressions;

namespace Utility.Filter.character;

public class ExperienceFilter : BaseFilter
{
    public ExperienceFilter()
    {
        FilterId = "Experience";
        Columns.AddRange(new [] {"Type", "Bonus"});
        Regexes = new Regex[]
        {
            new(@"^You gaine?d? (?<type>experience|party|raid)(?:\sexperience)?(?<bonus>\s\(with a bonus\))?!$",
                RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["type"].Value.Replace("experience", "solo")},
            {Columns[3], result.Groups["bonus"].Success.ToString()}
        };
        return data;
    }
}