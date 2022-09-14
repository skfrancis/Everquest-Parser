using System.Text.RegularExpressions;

namespace Utility.Filter.character;

public class AltAdvancementFilter : BaseFilter
{
    public AltAdvancementFilter()
    {
        FilterId = "AA";
        Columns.AddRange(new [] {"Gained", "Banked"});
        Regexes = new Regex[]
        {
            new(@"^You have gained (?<gained>an|\d+) ability point(?:.+)?!\s+You now have (?<banked>\d+) ability point(?:.+)?.$",
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
            {Columns[2], result.Groups["gained"].Value.Replace("an", "1")},
            {Columns[3], result.Groups["banked"].Value}
        };
        return data;
    }
}