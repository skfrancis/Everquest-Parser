using System.Text.RegularExpressions;

namespace Filter.general;

public class WhoFilter : BaseFilter
{
    public WhoFilter()
    {
        FilterId = "Who";
        Columns.AddRange(new [] {"Name", "Class", "Level"});
        Regexes = new Regex[]
        {
            new(@"^[A-Z\s]*\[(?:(ANONYMOUS)|(?<lvl>\d+) (?<class>[\w\s]+)|(?<lvl>\d+) .+? \((?<class>[\w\s]+)\))\](?:\s+(?<name>\w+))", 
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
            {Columns[2], result.Groups["name"].Value},
            {Columns[3], result.Groups["class"].Value},
            {Columns[4], result.Groups["lvl"].Value}
        };
        return data;    
    }
}