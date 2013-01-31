using SubSonic.Tests.Unit.Linq.DataProviderOverrides.ExpectedSqlStrings;
using SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases;

namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides
{
    public class OracleSqlStringTests : SqlStringTests
    {
        public OracleSqlStringTests()
        {
            SqlStrings = new OracleSqlStrings();
            Db = new TestDb(TestConfiguration.OracleTestConnectionString, DbClientTypeName.OracleDataAccess);
        }
    }
}