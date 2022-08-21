using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class FactionFilter : BaseFilter
{
    public FactionFilter()
    {
        Columns.AddRange(new [] {"Faction", "Amount"});
        Regexes = new Regex[]
        {
            new(@"^Your faction standing with (?<faction>[^.]+) has been adjusted by (?<amount>-?\d+)\.$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["faction"].Value},
            {Columns[3], result.Groups["amount"].Value}
        };
        return data;
    }
}