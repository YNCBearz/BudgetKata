using FluentAssertions;
using NSubstitute;

namespace BudgetKata;

[TestFixture]
public class BudgetServiceTest
{
    private IBudgetRepo _budgetRepo;
    private BudgetService _budgetService;

    [SetUp]
    public void SetUp()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void no_budgets()
    {
        GivenBudgets();
        TotalAmountShouldBe(0m, new DateTime(2022, 11, 01), new DateTime(2022, 11, 01));
    }

    [Test]
    public void period_with_whole_month()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(30m, new DateTime(2022, 11, 01), new DateTime(2022, 11, 30));
    }

    [Test]
    public void period_inside_budget_month()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(5m, new DateTime(2022, 11, 01), new DateTime(2022, 11, 05));
    }

    [Test]
    public void period_no_overlapping_before_budget_first_day()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(0m, new DateTime(2022, 10, 28), new DateTime(2022, 10, 30));
    }

    [Test]
    public void period_no_overlapping_after_budget_last_day()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(0m, new DateTime(2022, 12, 02), new DateTime(2022, 12, 05));
    }

    [Test]
    public void period_overlapping_budget_first_day()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(5m, new DateTime(2022, 10, 28), new DateTime(2022, 11, 05));
    }

    [Test]
    public void period_overlapping_budget_last_day()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 30
        });

        TotalAmountShouldBe(3m, new DateTime(2022, 11, 28), new DateTime(2022, 12, 05));
    }

    [Test]
    public void daily_amount_is_10()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 300
        });

        TotalAmountShouldBe(30m, new DateTime(2022, 11, 28), new DateTime(2022, 12, 05));
    }
    
    [Test]
    public void invalid_period()
    {
        GivenBudgets(new Budget
        {
            YearMonth = "202211",
            Amount = 300
        });

        TotalAmountShouldBe(0m, new DateTime(2022, 11, 10), new DateTime(2022, 11, 05));
    }

    [Test]
    public void multiple_budgets()
    {
        GivenBudgets(new Budget()
            {
                YearMonth = "202210",
                Amount = 31
            }, new Budget
            {
                YearMonth = "202211",
                Amount = 300
            },
            new Budget()
            {
                YearMonth = "202212",
                Amount = 3100
            });

        TotalAmountShouldBe(4+300+500, new DateTime(2022, 10, 28), new DateTime(2022, 12, 05));
    }

    private void GivenBudgets(params Budget[] budgets)
    {
        _budgetRepo.GetAll().Returns(budgets.ToList());
    }

    private void TotalAmountShouldBe(decimal expected, DateTime start, DateTime end)
    {
        var totalAmount = _budgetService.TotalAmount(start, end);
        totalAmount.Should().Be(expected);
    }
}