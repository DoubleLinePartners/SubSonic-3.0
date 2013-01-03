using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SubSonic.Linq.Structure
{
  public interface IQuery<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
  {
  }
}
