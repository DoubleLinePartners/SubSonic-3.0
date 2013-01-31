using System;
using System.Globalization;
using SubSonic.Configuration;
using SubSonic.Extensions;

namespace SubSonic.TypeConverters
{
    public interface IValueTypeConverter<in TSourceType, out TDestinationType>
    {
        TDestinationType Convert(TSourceType value);
    }

    public class DecimalToStringValueTypeConverter : IValueTypeConverter<decimal, string>
    {
        public string Convert(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class SingleToDecimalValueTypeConverter:IValueTypeConverter<Single, Decimal>
    {
        public decimal Convert(float value)
        {
            return new decimal(value);
        }
    }

    public class ByteArrayToGuidValueTypeConverter : IValueTypeConverter<byte[], Guid>
    {
        public Guid Convert(byte[] value)
        {
            return new Guid(value);
        }
    }

    public class StringToGuidValueTypeConverter : IValueTypeConverter<String, Guid>
    {
        public Guid Convert(string value)
        {
            return new Guid(value);
        }
    }

    public class DoubleToDecimalValueTypeConverter:IValueTypeConverter<Double, Decimal>
    {
        public decimal Convert(double value)
        {
            return new decimal(value);
        }
    }

    public class DecimalToInt32ValueTypeConverter: IValueTypeConverter<Decimal, int>
    {
        public int Convert(Decimal value)
        {
            return (int) value;
        }
    }

    public class Int16ToBooleanValueTypeConverter: IValueTypeConverter<short, bool>
    {
        public bool Convert(short value)
        {
            return value != 0;
        }
    }

    public class ValueTypeConverterService
    {
        public static object ChangeType(Object value, Type destinationType)
        {
            if (EquivalentTypes(value.GetType(), destinationType))
            {
                return value;
            }
            try
            {
                var valueTypeConverter = LocateValueTypeConverter(value.GetType(), destinationType);
                return valueTypeConverter.GetType().GetMethod("Convert").Invoke(valueTypeConverter, new[] {value});
            }
            catch (Castle.MicroKernel.ComponentNotFoundException)
            {
                return Convert.ChangeType(value, destinationType);   
            }
        }

        private static bool EquivalentTypes(Type sourceType, Type destinationType)
        {
            return sourceType.ToNotNullable() == destinationType.ToNotNullable();
        }

        private static object LocateValueTypeConverter(Type sourceType, Type destinationType)
        {
            var valueTypeConverterInterface = typeof (IValueTypeConverter<,>).MakeGenericType(sourceType,
                                                                                              destinationType
                                                                                                  .ToNotNullable());
            return Configurator.Container.Resolve(valueTypeConverterInterface);
        }

        public static Func<object, object> ChangeTypeFunction(Type sourceType, Type destinationType, bool returnNullIfValueTypeConverterIsNotFound = false)
        {
            if (EquivalentTypes(sourceType, destinationType))
            {
                return src => src;
            }
            try
            {
                var valueTypeConverter = LocateValueTypeConverter(sourceType, destinationType);
                return src => valueTypeConverter.GetType().GetMethod("Convert").Invoke(valueTypeConverter, new[] { src });
            }
            catch (Castle.MicroKernel.ComponentNotFoundException)
            {
                if (returnNullIfValueTypeConverterIsNotFound)
                    return null;
                return src => Convert.ChangeType(src, destinationType);
            }
        }
    }
}