using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using SubSonic.Tests.Unit.Linq.TestBases;
using SubSonic.Linq.Structure;

namespace SubSonic.Tests.Unit.Linq
{
  public class MemoryTests
  {
    [Fact]
    public void MemTestEmpty()
    {
      var db = new MemTestDB(TestConfiguration.MySqlTestConnectionString, DbClientTypeName.MySql);
      Assert.Equal(0, db.Categories.Count());
    }

    [Fact]
    public void MemTestPredefinedData()
    {
      var data = new List<TestClasses.Category>()
      {
        new TestClasses.Category()
        {
          CategoryID = 1,
          CategoryName = "1"
        }
      };

      var db = new MemTestDB(TestConfiguration.MySqlTestConnectionString, DbClientTypeName.MySql);
      db.Categories = new QueryList<TestClasses.Category>(data);
      Assert.Equal(1, db.Categories.Count());
      Assert.Equal(data.First().CategoryID, db.Categories.First().CategoryID);
    }

    [Fact]
    public void MemTestAddData()
    {
      var db = new MemTestDB(TestConfiguration.MySqlTestConnectionString, DbClientTypeName.MySql);
      ((QueryList<TestClasses.Category>)db.Categories).Data.Add(new TestClasses.Category()
        {
          CategoryID = 1,
          CategoryName = "1"
        });
      Assert.Equal(1, db.Categories.Count());
      Assert.Equal(1, db.Categories.First().CategoryID);
    }

    [Fact]
    public void MemTestJoin()
    {
      var categories = new List<TestClasses.Category>()
      {
        new TestClasses.Category()
        {
          CategoryID = 1,
          CategoryName = "1"
        }
      };

      var products = new List<TestClasses.Product>()
      {
        new TestClasses.Product()
        {
          CategoryID = 1,
          Discontinued = false,
          ProductID = 1,
          ProductName = "Prod One",
          Sku = Guid.NewGuid(),
          UnitPrice = 123.45M
        },
        new TestClasses.Product()
        {
          CategoryID = 2,
          Discontinued = false,
          ProductID = 2,
          ProductName = "Prod Two",
          Sku = Guid.NewGuid(),
          UnitPrice = 234.56M
        },
        new TestClasses.Product()
        {
          CategoryID = 1,
          Discontinued = false,
          ProductID = 3,
          ProductName = "Prod Three",
          Sku = Guid.NewGuid(),
          UnitPrice = 345.67M
        }
      };

      var db = new MemTestDB(TestConfiguration.MySqlTestConnectionString, DbClientTypeName.MySql);
      db.Categories = new QueryList<TestClasses.Category>(categories);
      db.Products = new QueryList<TestClasses.Product>(products);

      var data = from c in db.Categories
                 join p in db.Products on c.CategoryID equals p.CategoryID
                 where p.Discontinued == false
                 select p.ProductID;
      Assert.Equal(2, data.Count());
      Assert.True(data.Contains(1));
      Assert.False(data.Contains(2));
      Assert.True(data.Contains(3));
    }
  }
}
