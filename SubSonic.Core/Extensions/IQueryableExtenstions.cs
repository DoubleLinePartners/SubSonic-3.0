using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.Linq.Structure;

namespace SubSonic.Extensions
{
    /// <summary>
    /// Contains general extension methods for IQueryable
    /// </summary>
    public static class IQueryableExtenstions
    {
        public static string GetQueryText(this IQueryable query)
        {
            try
            {
                return (query.Provider as IQueryText).GetQueryText(query.Expression);
            }
            catch(Exception e)
            {
                return e.Message + "\r\n" + e.StackTrace;
            }
        }

        public static string PrintDebugInfo(this IQueryable query)
        {
            return string.Format(@"IQueryable.Expression.ToString
------------------------------
{0}

IQueryable.Expression Tree
--------------------------
{1}

DbQueryProvider.GetQueryPlan
----------------------------
{2}

SQL Query
---------
{3}",
    query.Expression.ToString(),
    query.Expression.PrintExpressionTree(),
    query.Provider is DbQueryProvider ? ((DbQueryProvider)query.Provider).GetQueryPlan(query.Expression) : "Unable to get query plan.",
    query.GetQueryText() ?? "Unable to build query text."
    );
        }
    }
}
