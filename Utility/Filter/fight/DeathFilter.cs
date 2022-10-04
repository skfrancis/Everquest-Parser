using System.Globalization;
using System.Text.RegularExpressions;

namespace Utility.Filter.fight;

public class DeathFilter : BaseFightFilter
{
    private readonly TextInfo _textInfo = new CultureInfo("en-US",false).TextInfo;
    
    public DeathFilter()
    {
        Regexes = new Regex[]
        {
            new(@"^(?<target>.+) (?:have|has) been slain by (?<source>.+)!$", RegexOptions.Compiled),
            new(@"^(?<source>.+) have slain (?<target>.+)!$", RegexOptions.Compiled),
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
            {Columns[2], _textInfo.ToTitleCase(result.Groups["source"].Value)},
            {Columns[3], _textInfo.ToTitleCase(result.Groups["target"].Value)},
            {Columns[4], string.Empty},
            {Columns[5], string.Empty},
            {Columns[6], string.Empty},
            {Columns[7], "death"}
        };
        return data;
    }
}