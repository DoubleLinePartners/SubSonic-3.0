using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using SubSonic.DataProviders;

namespace SubSonic.Tests.Unit.DataProviders
{
    /// <summary>
    /// This is intended to test the functionality of the <see cref="SubSonic.DataProviders.SharedDbConnectionScope"/>
    /// and the shared connection and transaction exposed by IDataProvider.
    /// </summary>
    public class SharedConnectionScopeTests
    {
        [Fact]
        public void Connection_Should_Be_Reused_When_Within_SharedConnectionScope_When_Provider_Is_Same()
        {
            var p1 = ProviderFactory.GetProvider(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
            using(var scope = new SharedDbConnectionScope(p1))
            {
                var p2 = ProviderFactory.GetProvider(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
                Assert.Same(p1.CurrentSharedConnection, p2.CurrentSharedConnection);
                Assert.Same(scope.CurrentConnection, p1.CurrentSharedConnection);
            }
        }

        [Fact]
        public void Connection_Should_Not_Be_Reused_When_Within_SharedConnectionScope_When_Provider_Is_Different()
        {
            var p1 = ProviderFactory.GetProvider(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
            using (var scope = new SharedDbConnectionScope(p1))
            {
                var p2 = ProviderFactory.GetProvider(TestConfiguration.SQLiteTestsConnectionString, DbClientTypeName.SqlLite);
                Assert.NotSame(p1.CurrentSharedConnection, p2.CurrentSharedConnection);
                Assert.Same(scope.CurrentConnection, p1.CurrentSharedConnection);
            }
        }

        [Fact]
        public void Nested_SharedConnectionScope_With_Different_Providers()
        {
            using (var sqlScope = new SharedDbConnectionScope(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql))
            {
                using (var sqliteScope = new SharedDbConnectionScope(TestConfiguration.SQLiteTestsConnectionString, DbClientTypeName.SqlLite))
                {
                    var p1 = ProviderFactory.GetProvider(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
                    var p2 = ProviderFactory.GetProvider(TestConfiguration.SQLiteTestsConnectionString, DbClientTypeName.SqlLite);
                    var p3 = ProviderFactory.GetProvider(TestConfiguration.MsSql2008TestConnectionString, DbClientTypeName.MsSql);
                    var p4 = ProviderFactory.GetProvider(TestConfiguration.SQLiteTestsConnectionString, DbClientTypeName.SqlLite);

                    Assert.Same(p1.CurrentSharedConnection, p3.CurrentSharedConnection);
                    Assert.Same(p2.CurrentSharedConnection, p4.CurrentSharedConnection);
                    Assert.NotSame(p1.CurrentSharedConnection, p2.CurrentSharedConnection);
                    Assert.NotSame(p3.CurrentSharedConnection, p4.CurrentSharedConnection);
                }
            }
        }
    }
}
