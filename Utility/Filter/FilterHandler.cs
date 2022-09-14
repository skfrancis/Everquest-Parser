using Utility.Filter.character;
using Utility.Filter.general;
using Utility.Filter.loot;
using Utility.Parser;

namespace Utility.Filter;

public class FilterHandler
{
    private readonly List<BaseFilter> _filters = new();

    public FilterHandler()
    {
        // character
        _filters.Add(new AchievementFilter());
        _filters.Add(new AltAdvancementFilter());
        _filters.Add(new CastingFilter());
        _filters.Add(new ExperienceFilter());
        _filters.Add(new FactionFilter());
        _filters.Add(new PetLeaderFilter());
        _filters.Add(new SkillsFilter());
        _filters.Add(new TradeskillFilter());
        
        // loot
        _filters.Add(new CoinFilter());
        
        // general
        _filters.Add(new ConsiderFilter());
        _filters.Add(new LocationFilter());
        _filters.Add(new SystemMessageFilter());
        _filters.Add(new WhoFilter());
        _filters.Add(new ZoneFilter());
    }

    public Dictionary<string, string>? ProcessFilters(ParsedLineObject parsedLine)
    {
        foreach(var filter in _filters)
        {
            var result = filter.Filter(parsedLine);
            if (result != null) return result;
        }
        return null;
    }
}