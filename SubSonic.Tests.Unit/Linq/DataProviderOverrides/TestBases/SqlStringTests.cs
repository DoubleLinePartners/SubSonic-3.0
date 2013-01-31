using System.Linq;
using SubSonic.Extensions;
using SubSonic.Tests.Unit.Linq.DataProviderOverrides.ExpectedSqlStrings;
using Xunit;

namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases
{
    public abstract class SqlStringTests : DataProviderOverridesTestBase
    {
        protected ISqlStrings SqlStrings;

        [Fact]
        public void SelectAll()
        {
            var result = Db.Orders.Where(order => true);
            AssertEqualIgnoringExtraWhitespaceAndCarriageReturn(SqlStrings.SelectAll, result.GetQueryText());
        }
    }
}