using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.Reflection
{
    internal static class ReflectionHelper
    {
        public static List<Type> GetDefaultInterfaces(this Type @this)
        {
            var orderedInterfaces = @this.GetTypeInfo().GetInterfaces()
                .OrderBy(@interface => @interface.AssemblyQualifiedName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var defaultInterfaces = orderedInterfaces
                .Where(@interface => @this.Name.Contains(GetInterfaceName(@interface)))
                .ToList();

            return defaultInterfaces;
        }

        public static List<Type> GetCustomAttributesInAssembly<T>(this Assembly assembly,
            bool includeNonGenericTypes = false)
        {
            var assignedTypes = assembly.GetTypes()
                .Where(type => GetCustomAttributesIncludingBaseInterfaces<T>(type).Any()
                               && type.GetTypeInfo().IsClass
                               && !type.GetTypeInfo().IsAbstract
                               && !type.GetTypeInfo().IsSealed)
                .ToList();

            if (!includeNonGenericTypes)
            {
                assignedTypes = assignedTypes
                    .Where(type => !type.GetTypeInfo().IsGenericType)
                    .ToList();
            }

            return assignedTypes.ToList();
        }

        private static string GetInterfaceName(MemberInfo @interface)
        {
            var name = @interface.Name;
            if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
            {
                name = name.Substring(1);
            }

            return name;
        }

        private static IEnumerable<T> GetCustomAttributesIncludingBaseInterfaces<T>(Type type)
        {
            var attributeType = typeof(T);

            return type.GetCustomAttributes(attributeType, true)
                .Union(type.GetInterfaces()
                    .SelectMany(interfaceType => interfaceType.GetCustomAttributes(attributeType, true)))
                .Distinct()
                .Cast<T>();
        }
    }
}