using System.Text.RegularExpressions;

namespace Filter.general;

public class SystemMessageFilter : BaseFilter
{
    public SystemMessageFilter()
    {
        FilterId = "System";
        Columns.AddRange(new [] {"Message"});
        Regexes = new Regex[]
        {
            new(@"^<SYSTEMWIDE_MESSAGE>:\s?(?<message>.+?)$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {"FilterId", FilterId},
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["message"].Value}
        };
        return data;  
    }
}