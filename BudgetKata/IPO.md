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
## Test Cases
- no budgets

**Given**

|YearMonth| Amount |
|:---|:-------|
||     |

|When|Then| Driven        |
|:---|:---|:--------------|
|2022/10/01 - 2022/10/31|0| Api Structure |

---

- period with whole month

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

|When| Then | Driven     |
|:---|:-----|:-----------|
|2022/10/01 - 2022/10/31| 31   | BudgetRepo |

---

- period inside budget month

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven       |
|:------------------------|:-----|:-------------|
| 2022/10/01 - 2022/10/05 | 5    | IntervalDays |

---

- period no overlap before budget first day

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven            |
|:------------------------|:-----|:------------------|
| 2022/09/28 - 2022/09/29 | 0    | Budget.FirstDay() |

---

- period no overlap after budget last day

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven           |
|:------------------------|:-----|:-----------------|
| 2022/11/02 - 2022/11/03 | 0    | Budget.LastDay() |

---

- period overlap budget first day

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven   |
|:------------------------|:-----|:---------|
| 2022/09/28 - 2022/10/05 | 5    | OverlappingStart |

---

- period overlap budget last day

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven         |
|:------------------------|:-----|:---------------|
| 2022/10/28 - 2022/11/03 | 4    | OverlappingEnd |

---

- period over whole month (expected to be green)
 
**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 31     |

| When                    | Then | Driven |
|:------------------------|:-----|:-------|
| 2022/09/28 - 2022/11/03 | 31   | x      |

---

- daily amount is 10

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 310    |

| When                    | Then | Driven                     |
|:------------------------|:-----|:---------------------------|
| 2022/10/28 - 2022/11/03 | 40   | Budget.Days(), DailyAmount |

---

- multiple budgets

**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202209    | 30  |
| 202210    | 310    |
| 202211    | 3000   |

| When                    | Then          | Driven |
|:------------------------|:--------------|:-------|
| 2022/09/28 - 2022/11/03 | 3 + 310 + 300 | Sum    |

---

- invalid period
 
**Given**

| YearMonth | Amount |
|:----------|:-------|
| 202210    | 310    |

| When                    | Then | Driven         |
|:------------------------|:-----|:---------------|
| 2022/10/03 - 2022/10/01 | 0    | Invalid Period |
