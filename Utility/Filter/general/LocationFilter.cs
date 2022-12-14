using System.Text.RegularExpressions;

namespace Utility.Filter.general;

public class LocationFilter : BaseFilter
{
    public LocationFilter()
    {
        FilterId = "Location";
        Columns.AddRange(new [] {"Y", "X", "Z"});
        Regexes = new Regex[]
        {
            new(@"^Your Location is (?<Y>-?\d+.+?), (?<X>-?\d+.+?), (?<Z>-?\d+.+?)$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["Y"].Value},
            {Columns[3], result.Groups["X"].Value},
            {Columns[4], result.Groups["Z"].Value}
        };
        return data;    
    }
}