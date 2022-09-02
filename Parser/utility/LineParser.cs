﻿using Serilog;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Parser.utility;

public static class LineParser
{
    private const string DateFormat = "ddd MMM dd HH:mm:ss yyyy";
    private static readonly Regex LineRegex = new (@"^\[(?<timestamp>.*?)] (?<text>.*?)$", RegexOptions.Compiled);

    public static ParsedLine Parse(string logLine)
    {
        var timeStamp = DateTime.MinValue;
        var text = string.Empty;
        var failedParse = new ParsedLine {Timestamp = timeStamp, Text = text};
        
        var result = LineRegex.Match(logLine);
        if (result.Success)
        {
            var dateParsed = DateTime.TryParseExact(result.Groups["timestamp"].Value, DateFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsedDateTime);
            if (dateParsed)
            {
                Log.Debug("Parsed Data: [Timestamp: {Timestamp}, Log Text: {LogText}]", 
                    parsedDateTime, result.Groups["text"].Value);
                return new ParsedLine(parsedDateTime, result.Groups["text"].Value);
            }
        }
        Log.Logger.Error("Log line parse failed: [{LogLine}]", logLine);
        return failedParse;
    }
}