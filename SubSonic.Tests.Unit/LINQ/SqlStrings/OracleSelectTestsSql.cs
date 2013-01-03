using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubSonic.Tests.Unit.Linq.SqlStrings {
    public class OracleSelectTestsSql : ISelectTestsSql {
        public string All_With_SubQuery
        {
            get {
                return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE NOT EXISTS( SELECT NULL
                          FROM Orders t1
                         WHERE ((t1.CustomerID = t0.CustomerID) AND
                               NOT (t1.OrderDate > to_date('2008-05-01T00:00:00','YYYY-MM-DD""T""HH24:MI:SS'))) )
            ";
            }
        }

        public string Any_With_Collection
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE ((t0.CustomerID = 'TEST1') OR (t0.CustomerID = 'TEST2'))
            ";}
        }

        public string Any_With_Collection_One_False
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE ((t0.CustomerID = 'ABCDE') OR (t0.CustomerID = 'TEST1'))
            ";}
        }

        public string Contains_Resolves_Literal
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                  WHERE (t0.ContactName LIKE '%' || 'har' || '%')
            ";}
        }

        public string Contains_With_LocalCollection_2_True
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE t0.CustomerID IN ('TEST2', 'TEST1')
            ";}
        }

        public string Contains_With_LocalCollection_OneFalse
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE t0.CustomerID IN ('ABCDE', 'TEST1')
            ";}
        }

        public string Contains_With_Subquery
        {
            get { return @"
                SELECT t0.Address,
                       t0.City,
                       t0.CompanyName,
                       t0.ContactName,
                       t0.Country,
                       t0.CustomerID,
                       t0.Region
                  FROM Customers t0
                 WHERE t0.CustomerID IN ( SELECT t1.CustomerID FROM Orders t1 )
            "; }
        }

        public string Count_Distinct
        {
            get { return "SELECT DISTINCT t0.City FROM Customers t0"; }
        }

        public string Count_No_Args
        {
            get { return "SELECT t0.OrderID FROM Orders t0"; }
        }

        public string Distinct_GroupBy
        {//actually correct
            /*get { return @"
                SELECT t0.CustomerID
                  FROM (SELECT DISTINCT t1.CustomerID,
                                        t1.Orderdate,
                                        t1.Orderid,
                                        t1.Requireddate,
                                        t1.Shippeddate
                          FROM Orders t1) t0
                 group by t0.CustomerID
            "; }*/
            get { return "SELECT t0.CustomerID FROM ( SELECT DISTINCT t1.CustomerID, t1.OrderDate, t1.OrderID, t1.RequiredDate, t1.ShippedDate FROM Orders t1 ) t0 GROUP BY t0.CustomerID SELECT DISTINCT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate FROM Orders t0 WHERE ((t0.CustomerID IS NULL AND t1.CustomerID IS NULL) OR (t0.CustomerID = t1.CustomerID))"; }
        }

        public string Distinct_Should_Not_Fail
        {
            get { return @"
                SELECT DISTINCT t0.Address,
                                t0.City,
                                t0.CompanyName,
                                t0.ContactName,
                                t0.Country,
                                t0.CustomerID,
                                t0.Region
                FROM Customers t0
            "; }
        }

        public string Distinct_Should_Return_11_For_Scalar_CustomerCity
        {
            get { return "SELECT DISTINCT t0.City FROM Customers t0"; }
        }

        public string Distinct_Should_Return_69_For_Scalar_CustomerCity_Ordered
        {
            get { return "SELECT DISTINCT t0.City FROM Customers t0 ORDER BY t0.City"; }
        }

        public string GroupBy_Basic
        {
            get { return @"SELECT t0.City 
                            FROM Customers t0
                            GROUP BY t0.City 
                          SELECT t0.Address, 
                                 t0.City, 
                                 t0.CompanyName, 
                                 t0.ContactName, 
                                 t0.Country, 
                                 t0.CustomerID, 
                                 t0.Region
                            FROM Customers t0
                            WHERE ((t0.City IS NULL AND t1.City IS NULL) 
                               OR (t0.City = t1.City))";
            }
        }

        public string GroupBy_Distinct
        {//actually correct
            //get { return "SELECT DISTINCT t0.CustomerID FROM ( SELECT t1.CustomerID FROM Orders t1 GROUP BY t1.CustomerID ) t0 ";}
            get { return "SELECT DISTINCT t0.CustomerID FROM ( SELECT t1.CustomerID FROM Orders t1 GROUP BY t1.CustomerID ) t0 SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate FROM Orders t0 WHERE ((t0.CustomerID IS NULL AND t1.CustomerID IS NULL) OR (t0.CustomerID = t1.CustomerID))"; }
        }

        public string GroupBy_SelectMany
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM ( SELECT t1.City FROM Customers t1 GROUP BY t1.City ) t2 INNER JOIN Customers t0 ON ((t0.City IS NULL AND t2.City IS NULL) OR (t0.City = t2.City))"; }
        }

        public string GroupBy_Sum
        {
            get { return "SELECT SUM(t0.OrderID) agg1, MIN(t0.OrderID) agg2, MAX(t0.OrderID) agg3, AVG(t0.OrderID) agg4 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string GroupBy_Sum_With_Element_Selector_Sum_Max
        {
            get { return "SELECT SUM(t0.OrderID) agg1, MAX(t0.OrderID) agg2 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string GroupBy_Sum_With_Result_Selector
        {
            get { return "SELECT SUM(t0.OrderID) c0, MIN(t0.OrderID) c1, MAX(t0.OrderID) c2, AVG(t0.OrderID) c3 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string GroupBy_With_Anon_Element
        {
            get { return "SELECT SUM(t0.OrderID) agg1 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string GroupBy_With_Element_Selector
        {
            get { return "SELECT t0.CustomerID FROM Orders t0 GROUP BY t0.CustomerID SELECT t0.OrderID FROM Orders t0 WHERE ((t0.CustomerID IS NULL AND t1.CustomerID IS NULL) OR (t0.CustomerID = t1.CustomerID))"; }
        }

        public string GroupBy_With_Element_Selector_Sum
        {
            get { return "SELECT SUM(t0.OrderID) agg1 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string GroupBy_With_OrderBy
        {
            get { return "SELECT SUM(t0.OrderID) agg1 FROM Orders t0 GROUP BY t0.CustomerID"; }
        }

        public string Join_To_Categories
        {
            get { return "SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Categories t1 INNER JOIN Products t0 ON (t1.CategoryID = t0.CategoryID)"; }
        }

        public string OrderBy_CustomerID
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 ORDER BY t0.CustomerID"; }
        }

        public string OrderBy_CustomerID_Descending
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 ORDER BY t0.CustomerID DESC"; }
        }

        public string OrderBy_CustomerID_Descending_ThenBy_City
        {
            get { return "SELECT t0.City FROM Customers t0 ORDER BY t0.CustomerID DESC, t0.City"; }
        }

        public string OrderBy_CustomerID_Descending_ThenByDescending_City
        {
            get { return "SELECT t0.City FROM Customers t0 ORDER BY t0.CustomerID DESC, t0.City DESC"; }
        }

        public string OrderBy_CustomerID_OrderBy_Company_City
        {
            get { return "SELECT t0.City FROM Customers t0 ORDER BY t0.City, t0.CompanyName"; }
        }

        public string OrderBy_CustomerID_ThenBy_City
        {
            get { return @"SELECT t0.City 
                             FROM Customers t0 
                         ORDER BY t0.CustomerID, 
                                  t0.City"; }
        }

        public string OrderBy_CustomerID_With_Select
        {
            get
            {
                return @"SELECT t0.Address
FROM Customers t0
ORDER BY t0.CustomerID";
            }
        }

        public string OrderBy_Join
        {
            get { return "SELECT t0.CustomerID, t1.OrderID FROM Customers t0 INNER JOIN Orders t1 ON (t0.CustomerID = t1.CustomerID) ORDER BY t0.CustomerID, t1.OrderID"; }
        }

        public string OrderBy_SelectMany
        {
            get { return "SELECT t0.CustomerID, t1.OrderID FROM Customers t0 CROSS JOIN Orders t1 WHERE (t0.CustomerID = t1.CustomerID) ORDER BY t0.CustomerID, t1.OrderID"; }
        }

        public string Paging_With_Skip_Take
        {
            get { return "select * from ( select t1.*, ROWNUM rn from (SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Products t0 ORDER BY t0.ProductID ) t1 ) where rn >= 10 AND rn <= 30"; }
        }

        public string Paging_With_Take
        {
            get { return "select * from ( select t1.*, ROWNUM rn from (SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Products t0 ) t1 ) where rn >= 0 AND rn <= 20"; }
        }

        public string Select_0_When_Set_False
        {
            get { return "SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Products t0 WHERE 0 <> 0"; }
        }

        public string Select_100_When_Set_True
        {
            get { return "SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Products t0 WHERE 1 <> 0"; }
        }

        public string Select_Anon_Constant_Int
        {
            get { return "SELECT NULL FROM Products t0"; }
        }

        public string Select_Anon_Constant_NullString
        {
            get { return "SELECT NULL FROM Products t0"; }
        }

        public string Select_Anon_Empty
        {
            get { return "SELECT NULL FROM Products t0"; }
        }

        public string Select_Anon_Literal
        {
            get { return "SELECT NULL FROM Products t0"; }
        }

        public string Select_Anon_Nested
        {
            get { return "SELECT t0.ProductName, t0.UnitPrice FROM Products t0"; }
        }

        public string Select_Anon_One
        {
            get { return @"SELECT t0.ProductName 
                             FROM Products t0"; }
        }

        public string Select_Anon_One_And_Object
        {
            get { return "SELECT t0.ProductName, t0.CategoryID, t0.Discontinued, t0.ProductID, t0.Sku, t0.UnitPrice FROM Products t0"; }
        }

        public string Select_Anon_Three
        {
            get { return "SELECT t0.ProductName, t0.UnitPrice, t0.Discontinued FROM Products t0"; }
        }

        public string Select_Anon_Two
        {
            get { return "SELECT t0.ProductName, t0.UnitPrice FROM Products t0"; }
        }

        public string Select_Anon_With_Local
        {
            get { return "SELECT NULL FROM Products t0"; }
        }

        public string Select_Nested_Collection
        {
            get { return "SELECT ( SELECT COUNT(*) FROM OrderDetails t0 WHERE (t0.ProductID = t1.ProductID) ) c0 FROM Products t1 WHERE (t1.ProductID = 1)"; }
        }

        public string Select_Nested_Collection_With_AnonType
        {
            //this query is bunk
            get { return "SELECT t0.ProductID FROM Products t0 WHERE (t0.ProductID = 1) SELECT t0.ProductID, t1.Test, t1.Discount, t1.OrderDetailID, t1.OrderID, t1.ProductID ProductID1, t1.Quantity, t1.UnitPrice FROM Products t0 LEFT OUTER JOIN ( SELECT t2.Discount, t2.OrderDetailID, t2.OrderID, t2.ProductID, t2.Quantity, 1 Test, t2.UnitPrice FROM OrderDetails t2 ) t1 ON (t1.ProductID = t0.ProductID) WHERE (t0.ProductID = 1)"; }
        }

        public string Select_On_Self
        {
            get { return "SELECT t0.CategoryID, t0.Discontinued, t0.ProductID, t0.ProductName, t0.Sku, t0.UnitPrice FROM Products t0"; }
        }

        public string Select_Scalar
        {
            get { return @"SELECT t0.ProductName 
                             FROM Products t0"; }
        }

        public string SelectMany_Customer_Orders
        {
            get { return "SELECT t0.ContactName, t1.OrderID FROM Customers t0 CROSS JOIN Orders t1 WHERE (t0.CustomerID = t1.CustomerID)"; }
        }

        public string Where_Resolves_String_EndsWith_Literal
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (t0.City LIKE CONCAT('%','10'))"; }
        }

        public string Where_Resolves_String_EndsWith_OtherColumn
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (t0.ContactName LIKE CONCAT('%',t0.ContactName))"; }
        }

        public string Where_Resolves_String_IsNullOrEmpty
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (t0.City IS NULL OR t0.City = '')"; }
        }

        public string Where_Resolves_String_Length
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (LENGTH(t0.City) = 7)"; }
        }

        public string Where_Resolves_String_StartsWith_Literal
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (t0.ContactName LIKE CONCAT('C','%'))"; }
        }

        public string Where_Resolves_String_StartsWith_OtherColumn
        {
            get { return "SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region FROM Customers t0 WHERE (t0.ContactName LIKE CONCAT(t0.ContactName,'%'))"; }
        }

        public string Ora_Where_Any_Generates_Valid_Query
        {
            get
            {
                return @"SELECT (CASE WHEN (EXISTS(
  SELECT NULL 
  FROM Customers t0
  WHERE (t0.CompanyName = 'foo')
  )) THEN 1 ELSE 0 END) value
FROM DUAL";
            }
        }

        public string Ora_Any_Generates_Valid_Query
        {
            get
            {
                return @"SELECT (CASE WHEN (EXISTS(
  SELECT NULL 
  FROM Customers t0
  WHERE (t0.CompanyName = 'foo')
  )) THEN 1 ELSE 0 END) value
FROM DUAL";
            }
        }

        public string Ora_Predecate_With_EqualsTrue_Generates_Valid_Query
        {
            get
            {
                return @"SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region
FROM Customers t0
WHERE ((CASE WHEN ((t0.ContactName LIKE CONCAT('C','%'))) THEN 1 ELSE 0 END) = 1)";
            }
        }

        public string Ora_Predecate_Without_EqualsTrue_Generates_Valid_Query
        {
            get
            {
                return @"SELECT t0.Address, t0.City, t0.CompanyName, t0.ContactName, t0.Country, t0.CustomerID, t0.Region
FROM Customers t0
WHERE (t0.ContactName LIKE CONCAT('C','%'))";
            }
        }
    }
}
