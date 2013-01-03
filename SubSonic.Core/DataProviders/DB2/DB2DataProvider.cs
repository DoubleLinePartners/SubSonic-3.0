using System;
using System.Data.Common;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;

namespace SubSonic.DataProviders.DB2
{
    /// <summary>
    /// This data provider is for IBM DB2, using the IBM Data Server Client.
    /// The provider string is typically "IBM.Data.DB2"
    /// </summary>
    public class DB2DataProvider : DbDataProvider
    {
        public DB2DataProvider(string connectionString, string providerName)
            : base(connectionString, providerName)
        {}

        public override ISchemaGenerator SchemaGenerator
        {
            get { return new DB2Schema(); }
        }

        public override IQueryLanguage QueryLanguage
        {
            get { return new DB2Language(this); }
        }

        public override string InsertionIdentityFetchString
        {
            get { return "; SELECT IDENTITY_VAL_LOCAL() AS new_id FROM SYSIBM.SYSDUMMY1"; }
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
            string qualifiedFormat = String.IsNullOrEmpty(sp.SchemaName) ? "{1}" : "{0}.{1}";
            return String.Format(qualifiedFormat, sp.SchemaName, sp.Name);
        }

        public override ISqlGenerator GetSqlGenerator(SqlQuery query)
        {
            return new DB2Generator(query);
        }

        #region Shared connection and transaction handling

        [ThreadStatic]
        private static DbConnection __sharedConnection;
        [ThreadStatic]
        private static DbTransaction __sharedTransaction;

        /// <summary>
        /// Gets or sets the current shared connection.
        /// </summary>
        /// <value>The current shared connection.</value>
        public override DbConnection CurrentSharedConnection
        {
            get { return __sharedConnection; }

            protected set
            {
				if (value == null)
                {
					if (__sharedConnection != null)
					{
						__sharedConnection.Dispose();
						__sharedConnection = null;
					}
                }
                else
                {
                    __sharedConnection = value;
                    __sharedConnection.Disposed += __sharedConnection_Disposed;
                }
            }
        }

        public override DbTransaction CurrentSharedTransaction
        {
            get { return __sharedTransaction; }

            set
            {
                if (__sharedTransaction != null)
                {
                    try
                    {
                        __sharedTransaction.Dispose();
                    }
                    catch
                    {
                        // ignore errors.
                    }
                }
                __sharedTransaction = value;
            }
        }

        private static void __sharedConnection_Disposed(object sender, EventArgs e)
        {
            __sharedConnection = null;
        }

        #endregion
    }
}
