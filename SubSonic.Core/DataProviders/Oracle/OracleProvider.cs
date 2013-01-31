using System;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;

namespace SubSonic.DataProviders.Oracle
{
    public class OracleProvider : DbDataProvider, IDataProvider
    {
        private const string InsertionIdentityFetchStringValue = "";

        public override string InsertionIdentityFetchString
        {
            get { return InsertionIdentityFetchStringValue; }
        }

        public OracleProvider(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public override ISchemaGenerator SchemaGenerator
        {
            get { return new OracleSchema(); }
        }


        public override ISqlGenerator GetSqlGenerator(SqlQuery query)
        {
            return new OracleGenerator(query);
        }


        public override string QualifyTableName(ITable tbl)
        {
            string qualifiedFormat = String.IsNullOrEmpty(tbl.SchemaName) ? "{1}" : "{0}.{1}";
            return String.Format(qualifiedFormat, tbl.SchemaName, tbl.Name);
        }

        public override string QualifyColumnName(IColumn column)
        {
            string qualifiedFormat = String.IsNullOrEmpty(column.SchemaName) ? "{1}.{2}" : "{0}.{1}.{2}";
            return String.Format(qualifiedFormat, column.Table.SchemaName, column.Table.Name, column.Name);
        }

        public override string QualifySPName(IStoredProcedure sp)
        {
            string qualifiedFormat = String.IsNullOrEmpty(sp.SchemaName) ? "\"{1}\"" : "\"{0}\".\"{1}\"";
            return String.Format(qualifiedFormat, sp.SchemaName, sp.Name);
        }

        public override IQueryLanguage QueryLanguage
        {
            get { return new OracleLanguage(this); }
        }

        public new string ParameterPrefix
        {
            get { return ":"; }
        }

        [ThreadStatic] private static DbConnection _sharedConnection;
        [ThreadStatic] private static DbTransaction _sharedTransaction;

        /// <summary>
        /// Gets or sets the current shared connection.
        /// </summary>
        /// <value>The current shared connection.</value>
        public override DbConnection CurrentSharedConnection
        {
            get { return _sharedConnection; }

            protected set
            {
                if (value == null)
                {
                    if (_sharedConnection != null)
                    {
                        _sharedConnection.Dispose();
                        _sharedConnection = null;
                    }
                }
                else
                {
                    _sharedConnection = value;
                    _sharedConnection.Disposed += SharedConnectionDisposed;
                }
            }
        }

        public override DbTransaction CurrentSharedTransaction
        {
            get { return _sharedTransaction; }

            set
            {
                if (_sharedTransaction != null)
                {
                    try
                    {
                        _sharedTransaction.Dispose();
                    }
                    catch (Exception)
                    {
                        Log.WriteLine("Pokemon Exception Caught!"); // ignore errors.
                    }
                }
                _sharedTransaction = value;
            }
        }

        private static void SharedConnectionDisposed(object sender, EventArgs e)
        {
            _sharedConnection = null;
        }
    }
}