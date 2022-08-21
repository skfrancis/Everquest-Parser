﻿using System.Text.RegularExpressions;

namespace Parser.filters.character;

public class PetLeaderFilter : BaseFilter
{
    public PetLeaderFilter()
    {
        Columns.AddRange(new [] {"Leader", "Pet"});
        Regexes = new Regex[]
        {
            new(@"(?<pet>^.+) says, 'My leader is (?<leader>\w+)\.'$", RegexOptions.Compiled)
        };
    }
    protected override Dictionary<string, string> ProcessResult(DateTime timeStamp, Match result)
    {
        var data = new Dictionary<string, string>
        {
            {Columns[0], timeStamp.Date.ToShortDateString()},
            {Columns[1], timeStamp.TimeOfDay.ToString()},
            {Columns[2], result.Groups["leader"].Value},
            {Columns[3], result.Groups["pet"].Value}
        };
        return data;
    }
}