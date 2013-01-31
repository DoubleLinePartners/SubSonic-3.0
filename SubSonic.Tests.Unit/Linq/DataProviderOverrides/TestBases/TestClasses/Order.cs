using System;
using SubSonic.DataProviders.Oracle;
using SubSonic.DataProviders.SqlServer;
using SubSonic.SqlGeneration.Schema;

namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases.TestClasses
{
    [SubSonicDataProviderTableNameOverride(typeof(SqlServerProvider), "SqlServerOrders")]
    [SubSonicDataProviderTableNameOverride(typeof(OracleProvider), "OracleOrders")]
    [SubSonicDataProviderTableNameOverride(typeof(OracleDataAccessProvider), "OracleOrders")]
    public class Order
    {
        [SubSonicPrimaryKey]
        [SubSonicDataProviderColumnNameOverride(typeof(OracleProvider), "OID")]
        [SubSonicDataProviderColumnNameOverride(typeof(OracleDataAccessProvider), "OID")]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        [SubSonicDataProviderColumnNameOverride(typeof(OracleProvider), "CID")]
        [SubSonicDataProviderColumnNameOverride(typeof(OracleDataAccessProvider), "CID")]
        public string CustomerID { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}