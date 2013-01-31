
namespace SubSonic.Tests.Unit.Linq.SqlStrings
{
    public class OracleDateTestsSql : IDateTestsSql
    {
        #region Properties (9)

        public string DateTime_Day
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR(t0.OrderDate, 'DD') = 5)";
            }
        }

        public string DateTime_DayOfWeek
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE ((TO_CHAR(t0.OrderDate, 'D') - 1) = 5)";
            }
        }

        public string DateTime_DayOfYear
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE ((TO_CHAR( t0.OrderDate, 'DDD') - 1) = 360)";
            }
        }

        public string DateTime_Hour
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR( t0.OrderDate, 'HH24') = 6)";
            }
        }

        public string DateTime_Millisecond
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR( t0.OrderDate, 'FF') = 200)";
            }
        }

        public string DateTime_Minute
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR( t0.OrderDate, 'MI') = 32)";
            }
        }

        public string DateTime_Month
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR(t0.OrderDate, 'MM') = 12)";
            }
        }

        public string DateTime_Second
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR( t0.OrderDate, 'SS') = 47)";
            }
        }

        public string DateTime_Year
        {
            get
            {
                return @"SELECT t0.CustomerID, t0.OrderDate, t0.OrderID, t0.RequiredDate, t0.ShippedDate
FROM Orders t0
WHERE (TO_CHAR(t0.OrderDate, 'YYYY') = 2007)";
            }
        }

        #endregion Properties
    }
}

