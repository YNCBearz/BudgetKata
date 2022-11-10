namespace BudgetKata;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    private DateTime Start { get; set; }
    private DateTime End { get; set; }

    public decimal GetOverLappingDays(Period period)
    {
        if (IsInvalid())
        {
            return 0;
        }
        
        if (Start > period.End || End < period.Start)
        {
            return 0;
        }

        var overlappingStart = Start > period.Start
            ? Start
            : period.Start;

        var overlappingEnd = End < period.End
            ? End
            : period.End;

        return (overlappingEnd - overlappingStart).Days + 1;
    }

    private bool IsInvalid()
    {
        return Start > End;
    }
}