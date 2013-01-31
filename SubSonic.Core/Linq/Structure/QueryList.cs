using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SubSonic.Linq.Structure
{
  public class QueryList<T> : IQuery<T>
  {
    private IList<T> _data;

    public QueryList()
    {
      _data = new List<T>();
    }

    public QueryList(IList<T> data)
    {
      _data = data;
    }

    public QueryList(params T[] data)
    {
      _data = new List<T>(data);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _data.GetEnumerator();
    }

    public Type ElementType
    {
      get { return typeof(T); }
    }

    public Expression Expression
    {
      get { return Expression.Constant(_data.AsQueryable()); }
    }

    public IQueryProvider Provider
    {
      get { return _data.AsQueryable().Provider; }
    }

    public IList<T> Data { get { return _data; } }
  }
}
