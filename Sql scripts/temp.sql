IF OBJECT_ID('tempdb..#IncomeCost') IS NOT NULL 
DROP TABLE #IncomeCost
GO

SELECT
    RT.[ActionName] as Action,
    YEAR(PR.[Date]) as Year,
    SUBSTRING(DATENAME(m,PR.[Date]), 1, 3) as Month,
    SUM(PR.[Amount]) as Amount
INTO #IncomeCost
FROM PredefinedRecords PR
INNER JOIN RecordTypes RT ON RT.Id=PR.TypeId
WHERE YEAR(PR.[Date]) = 2020 
GROUP BY Year(PR.[Date]), DATENAME(m,PR.[Date]), RT.[ActionName]



SELECT 
	--ROW_NUMBER() OVER ( ORDER BY [Year] ) AS Id,
    [Year],	
	(CASE WHEN [Action] = 'income' THEN 'Income' ELSE 'Cost' END) as Action,
    [Jan],
    [Feb],
    [Mar],
    [Apr],
    [May],
    [Jun],
    [Jul],
    [Aug],
    [Sep],
    [Oct],
    [Nov],
    [Dec]
FROM #IncomeCost

PIVOT
(
    SUM(Amount)
    FOR [Month]
    IN ([Jan],
        [Feb],
        [Mar],
        [Apr],
        [May],
        [Jun],
        [Jul],
        [Aug],
        [Sep],
        [Oct],
        [Nov],
        [Dec])
) as MonthWiseIncomeCost

UNION ALL

SELECT 
	--ROW_NUMBER() OVER ( ORDER BY [Year] ) AS Id,
    [Year],	
	(CASE WHEN [Action] = 'income' THEN 'Cumulative Income' ELSE 'Cumulative Cost' END) as Action,
    [Jan] as Jan,
    [Jan] + [Feb] as Feb,
    [Jan] + [Feb] + [Mar] as Mar,
    [Jan] + [Feb] + [Mar] + [Apr] as Apr,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] as May,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] as Jun,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] as Jul,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] + [Aug] as Aug,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] + [Aug] + [Sep] as Sep,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] + [Aug] + [Sep] + [Oct] as Oct,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] + [Aug] + [Sep] + [Oct] + [Nov] as Nov,
    [Jan] + [Feb] + [Mar] + [Apr] + [May] + [Jun] + [Jul] + [Aug] + [Sep] + [Oct] + [Nov] + [Dec] as Dec
FROM #IncomeCost

PIVOT
(
    SUM(Amount)
    FOR [Month]
    IN ([Jan],
        [Feb],
        [Mar],
        [Apr],
        [May],
        [Jun],
        [Jul],
        [Aug],
        [Sep],
        [Oct],
        [Nov],
        [Dec])
) as MonthWiseCumulativeIncomeCost;
