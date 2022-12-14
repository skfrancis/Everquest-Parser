using System.Text.RegularExpressions;

namespace Utility.Filter.character;

public class FactionFilter : BaseFilter
{
    public FactionFilter()
    {
        FilterId = "Faction";
        Columns.AddRange(new [] {"Faction", "Amount"});
        Regexes = new Regex[]
        {
            new(@"^Your faction standing with (?<faction>[^.]+) has been adjusted by (?<amount>-?\d+)\.$", RegexOptions.Compiled),
            new(@"^Your faction standing with (?<faction>[^.]+) could not possibly get any (?<amount>.+)\.$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var amount = result.Groups["amount"].Value;
        amount = amount.Replace("better", "max");
        amount = amount.Replace("worse", "min");
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["faction"].Value},
            {Columns[3], amount}
        };
        return data;
    }
}