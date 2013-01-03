using SubSonic.DataProviders;
using SubSonic.Tests.Linq;
using SubSonic.Tests.Linq.TestBases;
using Xunit;

namespace SubSonic.Tests.Linq
{
    /**
     *  Tests for Oracle using ODP.NET.
     *  
     *  Since not everyone has a full blown Oracle DB laying around for testing, you can use Oracle XE
     *   which is available from Oracle here: http://www.oracle.com/technology/software/products/database/xe/index.html
     *  At this time, XE is based on 10g and full-blown Oracle is 11g, but I think XE should be good enough
     *   for testing. The query language hasn't changed significantly. However, be forewarned; Oracle
     *   has managed to bloat the "Express Edition" to requiring at least 1.5 GB free disk space! Thanks guys!
     *   
     *  Also note that this is dependant on the Oracle Data Provider for .NET (ODP.NET) Oracle.DataAccess
     *   assembly which can also be obtained directly from Oracle.
     *   
     *  1) Install Oracle XE
     *  2) Set SYSTEM account pwd to 'system' (or change the connection string in <see cref="TestConfiguration"/>)
     *  3) Open Oracle XE web interface (http://127.0.0.1:8080/apex)
     *  4) Click the dropdown arrow on the "SQL" button, and choose: SQL Scripts | Upload
     *  5) Browse to and Upload: SubSonic\DbScripts\Northwind_Oracle.sql and Northwind_Oracle_Data.sql
     *  6) Click on the uploaded Northwind_Oracle script to open script editor. Click "Run" button. Check results for errors.
     *  7) Click on the uploaded Northwind_Oracle_Data script to open script editor. Click "Run" button. Check results for errors.
     */

    // [TestFixture]
    public class OracleSelectTests : SelectTests
    {
        public OracleSelectTests()
        {
            _db = new TestDB(TestConfiguration.OracleTestConnectionString, DbClientTypeName.Oracle);
            var setup = new Setup(_db.Provider);
            setup.DropTestTables();
            setup.CreateTestTable();
            setup.LoadTestData();
        }
    }
}
