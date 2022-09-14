namespace Utility.Filter.fight;

public abstract class BaseFightFilter : BaseFilter
{
    protected BaseFightFilter()
    {
        FilterId = "Fight";
        Columns.AddRange(new []{"Source", "Target", "Damage", "Amount", "Mod", "Type"});    
    }
}