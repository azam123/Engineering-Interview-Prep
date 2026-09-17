# SQL Interview Q&A

## Essential topics
- Joins: inner, left, right, and full.
- Aggregation with `GROUP BY` and `HAVING`.
- CTEs and recursive CTEs.
- Window functions such as `ROW_NUMBER()` and `LAG()`.
- Index design, execution plans, and statistics.
- Transactions and isolation levels.

```sql
WITH Ranked AS (
    SELECT EmployeeId, DepartmentId, Salary,
           ROW_NUMBER() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS rn
    FROM Employees
)
SELECT * FROM Ranked WHERE rn <= 3;
```

## Interview questions
**How do you tune a slow query?** Inspect the execution plan, verify indexes and selectivity, avoid unnecessary columns, reduce row counts early, check blocking and statistics, and measure before/after.

**What is SQL injection?** Untrusted input changes query meaning. Prevent it with parameterized queries, least privilege, validation, and safe ORM usage.

**Offset vs keyset pagination?** Offset is simple but becomes slower at large offsets and can be inconsistent under changes. Keyset pagination uses a stable indexed cursor and scales better.

## References
- [SQL Server documentation](https://learn.microsoft.com/en-us/sql/)
- [Query processing architecture](https://learn.microsoft.com/en-us/sql/relational-databases/query-processing-architecture-guide)
