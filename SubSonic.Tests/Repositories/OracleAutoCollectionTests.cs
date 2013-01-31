using SubSonic.DataProviders;

namespace SubSonic.Tests.Repositories
{
    public class OracleAutoCollectionTests : AutoCollectionTests
    {
        public OracleAutoCollectionTests(IDataProvider provider) : base(ProviderFactory.GetProvider("NorthwindOracle"))
        {
        }
    }
}