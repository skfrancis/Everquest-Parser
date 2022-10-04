using Utility.Filter.character;
using Utility.Filter.general;
using Utility.Filter.loot;
using Utility.Parser;

namespace Utility.Filter;

public class FilterHandler
{
    private static readonly List<BaseFilter> Filters = new();

    public FilterHandler()
    {
        // character
        Filters.Add(new AchievementFilter());
        Filters.Add(new AltAdvancementFilter());
        Filters.Add(new CastingFilter());
        Filters.Add(new ExperienceFilter());
        Filters.Add(new FactionFilter());
        Filters.Add(new PetLeaderFilter());
        Filters.Add(new SkillsFilter());
        Filters.Add(new TradeskillFilter());
        
        // loot
        Filters.Add(new CoinFilter());
        
        // general
        Filters.Add(new ConsiderFilter());
        Filters.Add(new LocationFilter());
        Filters.Add(new SystemMessageFilter());
        Filters.Add(new WhoFilter());
        Filters.Add(new ZoneFilter());
    }

    public static Dictionary<string, string>? ProcessFilters(ParsedLineObject parsedLine)
    {
        foreach(var filter in Filters)
        {
            var result = filter.Filter(parsedLine);
            if (result != null) return result;
        }
        return null;
    }
}