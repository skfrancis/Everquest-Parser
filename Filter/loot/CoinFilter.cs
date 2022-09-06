using System.Text.RegularExpressions;

namespace Filter.loot;

public class CoinFilter : BaseFilter
{
    public CoinFilter()
    {
        FilterId = "Coin";
        Columns.AddRange(new [] {"Looter", "Platinum", "Gold", "Silver", "Copper"});
        Regexes = new Regex[]
        {
            new(@"^(?:(?<looter>.+) received?|The master looter,(?<looter>.+), looted) (?:(\d+) (\w+?), )?(?:(\d+) (\w+?), )?(?:(\d+) (\w+?) and )?(?:(\d+) (\w+?))(?: from.+| as.+)?\.$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["looter"].Value},
            {Columns[3], string.Empty},
            {Columns[4], string.Empty},
            {Columns[5], string.Empty},
            {Columns[5], string.Empty}
        };
        data[result.Groups[8].Value] = result.Groups[7].Value;
        if (result.Groups[6].Success)
        {
            data[result.Groups[6].Value] = result.Groups[5].Value;
        }
        if (result.Groups[4].Success)
        {
            data[result.Groups[4].Value] = result.Groups[3].Value;
        }
        if (result.Groups[2].Success)
        {
            data[result.Groups[2].Value] = result.Groups[1].Value;
        }
        return data;    
    }
}