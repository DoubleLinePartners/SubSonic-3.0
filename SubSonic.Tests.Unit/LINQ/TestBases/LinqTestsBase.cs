using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubSonic.Tests.Unit.Linq.TestBases
{
    /// <summary>
	/// A base class for all Linq Tests
	/// </summary>
	public abstract class LinqTestsBase : SqlStringTestsBase
    {
		protected TestDB _db;
    }
}
