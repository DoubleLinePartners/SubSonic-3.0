using System;
using System.Linq;

namespace SubSonic.Extensions
{
    public static class TypeExtensions
    {
        public static Type ToNotNullable(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException("Argument must be an interface type.", "interfaceType");
            return type.GetInterfaces().ToList().Contains(interfaceType);
        }
    }
}