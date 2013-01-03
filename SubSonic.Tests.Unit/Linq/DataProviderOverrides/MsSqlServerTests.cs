using SubSonic.Tests.Unit.Linq.DataProviderOverrides.ExpectedSqlStrings;
using SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases;

namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides
{
    public class MsSqlServerSqlStringTests : SqlStringTests
    {
        public MsSqlServerSqlStringTests()
        {
            SqlStrings = new MsSqlServerSqlStrings();
            Db = new TestDb(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
        }
    }
}