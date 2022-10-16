using System.Globalization;

namespace Utility.Filter.fight;

public abstract class BaseFightFilter : BaseFilter
{
    protected readonly TextInfo FightTextInfo = new CultureInfo("en-US",false).TextInfo;
    protected BaseFightFilter()
    {
        FilterId = "Fight";
        Columns.AddRange(new []{"Source", "Target", "Amount", "Ability", "Mod", "Type"});    
    }
}