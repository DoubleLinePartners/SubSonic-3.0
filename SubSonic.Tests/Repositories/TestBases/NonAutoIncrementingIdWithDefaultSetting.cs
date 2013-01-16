﻿using System;
using SubSonic.DataProviders.Oracle;
using SubSonic.SqlGeneration.Schema;

namespace SubSonic.Tests.Repositories.TestBases
{
    [SubSonicDataProviderTableNameOverride(typeof(OracleDataProvider), "NAIIWDS")]
    public class NonAutoIncrementingIdWithDefaultSetting
    {
        [SubSonicPrimaryKey(false)]
        public int Id { get; set; }

        [SubSonicDefaultSetting("NN")]
        [SubSonicNullString]
        public String Name { get; set; }
    }
}
