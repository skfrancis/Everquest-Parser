using System.Text.RegularExpressions;

namespace Filter.character;

public class AchievementFilter : BaseFilter
{
    public AchievementFilter()
    {
        FilterId = "Achievement";
        Columns.AddRange(new [] {"Player", "Achievement"});
        Regexes = new Regex[]
        {
            new(@"^(?:Your guildmate )?(?<player>.+?)(?: have| has) completed(?: achievement:)? (?<achievement>.+?)(?:$| achievement.$)",
                RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["player"].Value},
            {Columns[3], result.Groups["achievement"].Value}
        };
        return data;        
    }
}