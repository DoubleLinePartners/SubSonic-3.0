using System;
using System.Linq;
using SubSonic.DataProviders;
using SubSonic.Query;
using SubSonic.Repository;
using SubSonic.Tests.Repositories.TestBases;
using Xunit;

namespace SubSonic.Tests.Repositories
{
    public class OracleSimpleRepositoryTests : SimpleRepositoryTests
    {
        private readonly IDataProvider _dataProvider;
        private const string CreateStatement = @"CREATE TABLE GWERKOS (ID CHAR(36) NOT NULL, GUIDASRAW16 RAW(16), GUIDASCHAR32 CHAR(32), GUIDASCHAR36 CHAR(36), CONSTRAINT ID_PK PRIMARY KEY (ID) ENABLE);";
        private const string DropStatement = @"DROP TABLE GWERKOS CASECADE CONSTRAINTS;";
        private readonly IRepository _repository;

        public OracleSimpleRepositoryTests() :
            base(ProviderFactory.GetProvider("NorthwindOracle"))
        {
            _dataProvider = ProviderFactory.GetProvider("NorthwindOracle");
            TestSupport.CleanTables(_dataProvider, new [] {"NAIIWDS"});
            SetUpGwerkosTable();
            _repository = new SimpleRepository(_dataProvider, SimpleRepositoryOptions.None);
        }

        [Fact]
        public void Should_Get_All_By_Guid_Backed_By_Raw16()
        {
            var guid = Guid.NewGuid();
            AddGwerkoRecords(5, guid);

            var query = _repository.All<Gwerko>().Where(gwerko => gwerko.GuidAsRaw16 == guid) as SubSonic.Linq.Structure.Query<Gwerko>;
            Console.WriteLine(query.QueryText);
            var result = query.ToList();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void Should_Get_All_By_Guid_Backed_By_Char32()
        {
            var guid = Guid.NewGuid();
            AddGwerkoRecords(5, guid);

            var query = _repository.All<Gwerko>().Where(gwerko => gwerko.GuidAsChar32 == guid) as SubSonic.Linq.Structure.Query<Gwerko>;
            Console.WriteLine(query.QueryText);
            var result = query.ToList();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void Should_Get_All_By_Guid_Backed_By_Char36()
        {
            var guid = Guid.NewGuid();
            AddGwerkoRecords(5, guid);

            var query = _repository.All<Gwerko>().Where(gwerko => gwerko.GuidAsChar36 == guid) as SubSonic.Linq.Structure.Query<Gwerko>;
            Console.WriteLine(query.QueryText);
            var result = query.ToList();

            Assert.Equal(5, result.Count);
        }

        private void AddGwerkoRecords(int numberOfRecords, Guid guid)
        {
            for (var i = 0; i < numberOfRecords; i++)
                _repository.Add(new Gwerko
                {
                    ID = Guid.NewGuid(),
                    GuidAsChar32 = guid,
                    GuidAsChar36 = guid,
                    GuidAsRaw16 = guid
                });
        }

        private void SetUpGwerkosTable()
        {
            TryExecute(() => new CodingHorror(_dataProvider, DropStatement).Execute());
            TryExecute(() => new CodingHorror(_dataProvider, CreateStatement).Execute());
        }

        private static void TryExecute(Action actionToExecute)
        {
            try
            {
                actionToExecute();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}