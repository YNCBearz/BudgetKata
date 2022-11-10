namespace BudgetKata;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal TotalAmount(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();

        if (budgets.Any())
        {
            return budgets.Sum(budget =>
            {
                var period = new Period(start, end);
                return budget.GetOverLappingAmount(period);
            });
        }

        return 0;
    }
}