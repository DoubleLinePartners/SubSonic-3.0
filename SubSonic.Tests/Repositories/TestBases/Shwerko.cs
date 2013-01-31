using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SubSonic.DataProviders.Oracle;
using SubSonic.SqlGeneration.Schema;

namespace SubSonic.Tests.Repositories.TestBases
{
    public class Shwerko : IShwerko
    {
        public int ID { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public DateTime ElDate { get; set; }
        public decimal SomeNumber { get; set; }

		[SubSonicIgnore]
		public string SomePropertyToTestIgnoreAttribute { get; set; }

        public int? NullInt { get; set; }
        public decimal? NullSomeNumber { get; set; }
        public DateTime? NullElDate { get; set; }
        public Guid? NullKey { get; set; }
        public int Underscored_Column { get; set; }
        public Salutation Salutation { get; set; }
        public Salutation? NullableSalutation { get; set; }
        public byte[] Binary { get; set; }
    }

    public class Gwerko : IGwerko
    {
        public Guid ID { get; set; }

        public Guid GuidAsChar36 { get; set; }

        [SubSonicDataProviderDbType(typeof(OracleDataAccessProvider), DbType.Binary, 16)]
        public Guid GuidAsRaw16 { get; set; }

        [SubSonicDataProviderDbType(typeof(OracleDataAccessProvider), DbType.String, 32)]
        public Guid GuidAsChar32 { get; set; }
    }

    public interface IGwerko
    {
        Guid GuidAsChar36 { get; }
        Guid GuidAsRaw16 { get;  }
        Guid GuidAsChar32 { get; }
    }
}
