namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides.ExpectedSqlStrings
{
    public class MsSqlServerSqlStrings : ISqlStrings
    {
        public string SelectAll
        {
            get { return "SELECT [t0].[CustomerID], [t0].[OrderDate], [t0].[OrderID], [t0].[RequiredDate], [t0].[ShippedDate] FROM [SqlServerOrders] AS t0 WHERE 1 <> 0"; }
        }
    }
}