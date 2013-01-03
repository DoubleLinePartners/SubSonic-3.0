using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic.SqlGeneration.Schema;
using SubSonic.DataProviders.Oracle;

namespace SubSonic.DataProviders {
	public class OracleDataAccessDataProvider : OracleDataProvider {
		public OracleDataAccessDataProvider(string connectionString, string providerName) : base(connectionString, providerName) {}

		
	}
}
