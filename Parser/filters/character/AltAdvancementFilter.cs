using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class AltAdvancementFilter : BaseFilter
{
    public AltAdvancementFilter()
    {
        FilterId = "AA";
        Columns.AddRange(new [] {"Gained", "Banked"});
        Regexes = new Regex[]
        {
            new(@"^You have gained (?<gained>\d+) ability point\(s\)!\s+You now have (?<banked>\d+) ability point\(s\).$",
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
            {Columns[2], result.Groups["gained"].Value},
            {Columns[3], result.Groups["banked"].Value}
            
        };
        return data;
    }
}