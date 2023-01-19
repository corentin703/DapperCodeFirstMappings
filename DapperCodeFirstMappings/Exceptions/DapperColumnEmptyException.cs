using System;
using System.Reflection;

namespace DapperCodeFirstMappings.Exceptions
{
    public class DapperColumnEmptyException : ArgumentException
    {
        public DapperColumnEmptyException(Type type, PropertyInfo property)
            : base($"Database column name can't be empty in {type.Name}.{property.Name} (class {type.FullName ?? $"{type.Namespace}.{type.Name}"})")
        {
            //
        }
    }
}