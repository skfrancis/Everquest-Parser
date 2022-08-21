namespace Parser.filters.fight;

public abstract class BaseFightFilter : BaseFilter
{
    protected BaseFightFilter()
    {
        Columns.AddRange(new []{"Source", "Target", "Amount", "Ability", "Mod", "Type"});    
    }
}