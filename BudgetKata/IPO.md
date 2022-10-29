# Input Process Output

---

| Input  | Process           | Output      |
|:-------|:------------------|:------------|
| Start  | API Structure     | TotalAmount |
| End    | BudgetRepo        |     |
| Budget | IntervalDays      |     |
|  | DailyAmount       |     |
|  | Budget.Days()     |     |
|  | OverlappingDays   |     |
|  | OverlappingStart  |     |
|  | Budget.FirstDay() |     |
|  | OverlappingEnd    |     |
|  | Budget.LastDay()  |     |
|  | Sum               |     |
|  | Invalid Period    |     |

---

- no budgets

Given


|YearMonth| Amount |
|:---|:-------|
||     |

|When|Then| Driven        |
|:---|:---|:--------------|
|2022/10/01 - 2022/10/31|0| Api Structure |
