# Budget Kata

---

### Data Structure

```c#
public class Budget
{
    public int Amount { get; set; }

    public string YearMonth { get; set; }
}

public class AccountingService
{
    public decimal TotalAmount(DateTime start, DateTime end)
    {
    }
}
```

---

### Example

|YearMonth| Amount |
|:---|:-------|
|202210| 310    |
|202211| 3100   |

| Name           | Start      |End| TotalAmount |
|:---------------|:-----------|:---|:------------|
| One day        | 2022/10/01 |2022/10/01| 10          |
| Whole Month    | 2022/10/01 |2022/10/31| 310         |
| Multiple Month | 2022/10/01 | 2022/11/01| 310 + 100 |
| Invalid        | 2022/10/05 | 2022/10/01 | 0 |
