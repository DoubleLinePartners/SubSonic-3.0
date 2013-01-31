// 
//   SubSonic - http://subsonicproject.com
// 
//   The contents of this file are subject to the New BSD
//   License (the "License"); you may not use this file
//   except in compliance with the License. You may obtain a copy of
//   the License at http://www.opensource.org/licenses/bsd-license.php
//  
//   Software distributed under the License is distributed on an 
//   "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//   implied. See the License for the specific language governing
//   rights and limitations under the License.
// 
using System;
using System.Data;
using System.Linq;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Schema;

namespace SubSonic.SqlGeneration.Schema
{
    public interface IClassMappingAttribute
    {
        bool Accept(ITable table);
        void Apply(ITable table, IDataProvider provider);
    }

    public interface IPropertyMappingAttribute
    {
        bool Accept(IColumn column);
        void Apply(IColumn column, IDataProvider provider);
    }

	[AttributeUsage(AttributeTargets.Class)]
    public class SubSonicTableNameOverrideAttribute : Attribute, IClassMappingAttribute
	{
        public string TableName { get; set; }

        public SubSonicTableNameOverrideAttribute(string tableName)
        {
			TableName = tableName;
        }

        public bool Accept(ITable table)
        {
            return true;
        }

        public virtual void Apply(ITable table, IDataProvider provider)
        {
            table.Name = TableName;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubSonicDataProviderTableNameOverrideAttribute : SubSonicTableNameOverrideAttribute
    {
        private readonly Type _dataProviderType;

        public SubSonicDataProviderTableNameOverrideAttribute(Type dataProviderType, string tableName)
            : base(tableName)
        {
            if (!dataProviderType.GetInterfaces().ToList().Contains(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public override void Apply(ITable table, IDataProvider provider)
        {
            if (provider != null
                && provider.GetType() == _dataProviderType)
            {
                base.Apply(table, provider);
            }
        }

        public Type GetProviderType()
        {
            return _dataProviderType;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
	public class SubSonicColumnNameOverrideAttribute : Attribute, IPropertyMappingAttribute
	{
		public string ColumnName { get; set; }

		public SubSonicColumnNameOverrideAttribute(string tableName)
		{
			ColumnName = tableName;
		}

		public bool Accept(IColumn col)
		{
			return true;
		}

		public virtual void Apply(IColumn col, IDataProvider provider)
		{
			col.Name = ColumnName;
		}
	}

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SubSonicDataProviderColumnNameOverrideAttribute : SubSonicColumnNameOverrideAttribute
    {
        private readonly Type _dataProviderType;

        public SubSonicDataProviderColumnNameOverrideAttribute(Type dataProviderType, string tableName) : base(tableName)
        {
            if (!dataProviderType.Implements(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public override void Apply(IColumn column, IDataProvider provider)
        {
            if (provider != null
                && provider.GetType() == _dataProviderType)
            {
                base.Apply(column, provider);
            }
        }

        public Type GetProviderType()
        {
            return _dataProviderType;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicDbTypeAttribute: Attribute, IPropertyMappingAttribute
    {
        private readonly DbType _dbType;
        private readonly int _numericScale;
        private readonly int? _maxLegth;
        private readonly int _numericPrecision;

        public SubSonicDbTypeAttribute(DbType dbType)
        {
            _dbType = dbType;
        }

        public SubSonicDbTypeAttribute(DbType dbType, int maxLegth) : this(dbType)
        {
            _maxLegth = maxLegth;
        }

        public SubSonicDbTypeAttribute(DbType dbType, int numericScale, int numericPrecision): this(dbType)
        {
            _numericScale = numericScale;
            _numericPrecision = numericPrecision;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public virtual void Apply(IColumn column, IDataProvider provider)
        {
            column.DataType = _dbType;
            if (_maxLegth.HasValue)
                column.MaxLength = _maxLegth.Value;
            column.NumberScale = _numericScale;
            column.NumericPrecision = _numericPrecision;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicDataProviderDbTypeAttribute : SubSonicDbTypeAttribute
    {
        private readonly Type _dataProviderType;

        public SubSonicDataProviderDbTypeAttribute(Type dataProviderType, DbType dbType) : base(dbType)
        {
            if (!dataProviderType.Implements(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public SubSonicDataProviderDbTypeAttribute(Type dataProviderType, DbType dbType, int maxLegth) : base(dbType, maxLegth)
        {
            if (!dataProviderType.Implements(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public SubSonicDataProviderDbTypeAttribute(Type dataProviderType, DbType dbType, int numericScale, int numericPrecision)
            : base(dbType, numericScale, numericPrecision)
        {
            if (!dataProviderType.Implements(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public override void Apply(IColumn column, IDataProvider provider)
        {
            if (provider != null
                && provider.GetType() == _dataProviderType)
            {
                base.Apply(column, provider);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicNullStringAttribute : Attribute, IPropertyMappingAttribute 
    {
        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column, IDataProvider provider)
        {
            column.IsNullable = true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicLongStringAttribute : Attribute, IPropertyMappingAttribute
    {
        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column, IDataProvider provider)
        {
            column.MaxLength = 8001;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SubSonicUseSingularTableName : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicIgnoreAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicPrimaryKeyAttribute : Attribute, IPropertyMappingAttribute
    {
        public bool AutoIncrement { get; set; }

        public SubSonicPrimaryKeyAttribute() : this(true) {}

        public SubSonicPrimaryKeyAttribute(bool autoIncrement)
        {
            AutoIncrement = autoIncrement;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public virtual void Apply(IColumn column, IDataProvider provider)
        {
            column.IsPrimaryKey = true;
            column.IsNullable = false;
            if (column.IsNumeric)
                column.AutoIncrement = AutoIncrement;
            else if (column.IsString && column.MaxLength == 0)
                column.MaxLength = 255;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicDataProviderPrimaryKeyAttribute: SubSonicPrimaryKeyAttribute
    {
        private readonly Type _dataProviderType;

        public SubSonicDataProviderPrimaryKeyAttribute(Type dataProviderType) : this(dataProviderType, true)
        {
        }

        public SubSonicDataProviderPrimaryKeyAttribute(Type dataProviderType, bool autoIncrement) : base(autoIncrement)
        {
            if (!dataProviderType.GetInterfaces().ToList().Contains(typeof(IDataProvider)))
                throw new ArgumentException("Type must implement IDataProvider interface", "dataProviderType");
            _dataProviderType = dataProviderType;
        }

        public override void Apply(IColumn column, IDataProvider provider)
        {
            if (provider != null
                && provider.GetType() == _dataProviderType)
            {
                base.Apply(column, provider);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicStringLengthAttribute : Attribute, IPropertyMappingAttribute
    {
        public int Length { get; set; }
        
        public SubSonicStringLengthAttribute(int length)
        {
            Length = length;
        }

        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column, IDataProvider provider)
        {
            column.MaxLength = Length;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicNumericPrecisionAttribute : Attribute, IPropertyMappingAttribute
    {
        public int Precision { get; set; }
        public int Scale { get; set; }

        public SubSonicNumericPrecisionAttribute(int precision, int scale)
        {
            Scale = scale;
            Precision = precision;
        }

        public bool Accept(IColumn column)
        {
            return column.DataType == DbType.Decimal || column.DataType == DbType.Double;
        }

        public void Apply(IColumn column, IDataProvider provider)
        {
            column.NumberScale = Scale;
            column.NumericPrecision = Precision;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SubSonicDefaultSettingAttribute : Attribute, IPropertyMappingAttribute
    {
        public object DefaultSetting { get; set; }

        public SubSonicDefaultSettingAttribute(object defaultSetting)
        {
            DefaultSetting = defaultSetting;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public void Apply(IColumn column, IDataProvider provider)
        {
            column.DefaultSetting = DefaultSetting;
        }
    }
}