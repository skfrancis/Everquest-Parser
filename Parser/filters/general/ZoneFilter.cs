using System.Text.RegularExpressions;

namespace Parser.filters.general;

public class ZoneFilter : BaseFilter
{
    private readonly string[] _nonZones;
    
    public ZoneFilter()
    {
        FilterId = "Zone";
        Columns.AddRange(new [] {"Zone"});
        Regexes = new Regex[]
        {
            new (@"^You have entered (?<zone>.+)\.$", RegexOptions.Compiled)
        };
        _nonZones = new[]
        {
            "an area where levitation effects do not function",
            "an Arena (PvP) area",
            "an area where Bind Affinity is",
            "the Drunken Monkey stance adequately"
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        if (_nonZones.Any(nonZone => result.Groups[1].Value.Contains(nonZone)))
        {
            return new Dictionary<string, string>();
        }
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["zone"].Value}
        };
        return data;
    }
}