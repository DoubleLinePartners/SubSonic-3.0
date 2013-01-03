namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides.ExpectedSqlStrings
{
    public class OracleSqlStrings : ISqlStrings
    {
        public string SelectAll
        {
            get
            {
                return
                    "SELECT t0.CID, t0.OrderDate, t0.OID, t0.RequiredDate, t0.ShippedDate FROM OracleOrders t0 WHERE 1 <> 0";
            }
        }
    }
}