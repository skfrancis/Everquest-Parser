using System.Text.RegularExpressions;

namespace Filter.fight;

public class DeathFilter : BaseFightFilter
{
    public DeathFilter()
    {
        Regexes = new Regex[]
        {
            new(@"^(?<target>.+) (?:have|has) been slain by (?<source>.+)!$", RegexOptions.Compiled),
            new(@"^(?<source>You) have slain (?<target>.+)!$", RegexOptions.Compiled),
            new(@"^(?<source>(?<target>.+)) dies?d?\.$", RegexOptions.Compiled),
        };
    }
    
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups[1].Value},
            {Columns[3], result.Groups[2].Value},
            {Columns[4], string.Empty},
            {Columns[5], string.Empty},
            {Columns[6], string.Empty},
            {Columns[7], "death"}
        };
        return data;
    }
}