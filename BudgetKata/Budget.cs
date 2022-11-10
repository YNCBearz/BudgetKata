namespace BudgetKata;

public class Budget
{
    public string YearMonth { get; set; }
    public decimal Amount { get; set; }

    private DateTime FirstDay()
    {
        return DateTime.ParseExact($"{YearMonth}01", "yyyyMMdd", null);
    }

    private DateTime LastDay()
    {
        return FirstDay().AddMonths(1).AddDays(-1);
    }

    private Period CreatePeriod()
    {
        return new Period(FirstDay(),LastDay());
    }

    private decimal DailyAmount()
    {
        return Amount / Days();
    }

    private int Days()
    {
        return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
    }

    public decimal GetOverLappingAmount(Period period)
    {
        return period.GetOverLappingDays(CreatePeriod()) * DailyAmount();
    }
}