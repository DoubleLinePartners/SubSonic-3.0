using System;
using System.Linq.Expressions;
using System.Reflection;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases.TestClasses;

namespace SubSonic.Tests.Unit.Linq.DataProviderOverrides.TestBases
{
    public class TestDb : IQuerySurface
    {
        private readonly IDataProvider _provider;
        private readonly DbQueryProvider _queryProvider;

        public TestDb(string connection, string provider): this(ProviderFactory.GetProvider(connection, provider)) {}

        public TestDb(IDataProvider provider)
        {
            _provider = provider;
            _queryProvider = new DbQueryProvider(provider);

            Orders = new Query<Order>(provider);
        }

        public Query<Order> Orders { get; set; }

        public IDataProvider Provider
        {
            get { return _provider; }
        }

        public Select Select
        {
            get { return new Select(_provider); }
        }

        public Insert Insert
        {
            get { return new Insert(_provider); }
        }

        public DbQueryProvider QueryProvider
        {
            get { return _queryProvider; }
        }

        public SqlQuery Avg<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Count<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Max<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Min<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Variance<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Sum<T>(Expression<Func<T, object>> column)
        {
            throw new NotImplementedException();
        }

        public SqlQuery Delete<T>(Expression<Func<T, bool>> column) where T : new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(_provider);
            result = result.From<T>();
            var c = lamda.ParseConstraint();
            result.Constraints.Add(c);
            return result;
        }

        public Query<T> GetQuery<T>()
        {
            throw new NotImplementedException();
        }

        public ITable FindTable(string tableName)
        {
            new Migrator(Assembly.GetExecutingAssembly());
            return tableName == "Order" ? _provider.FindTable("Order") : null;
        }

        public Update<T> Update<T>() where T : new()
        {
            return new Update<T>(_provider);
        }
    }
}