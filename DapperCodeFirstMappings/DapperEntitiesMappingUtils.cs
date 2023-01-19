using Dapper;
using DapperCodeFirstMappings.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using DapperCodeFirstMappings.Exceptions;

namespace DapperCodeFirstMappings
{
    public static class DapperEntitiesMappingUtils
    {
        public static void LoadMappingsFromAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<DapperEntityAttribute>() == null) 
                    continue;

                LoadMappingFromEntityType(type);
            }
        }

        public static void LoadMappingFromEntityType(Type type)
        {
            SortedDictionary<string, PropertyInfo> columnToPropertyMapping = new SortedDictionary<string, PropertyInfo>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                DapperColumnAttribute columnAttribute = property.GetCustomAttribute<DapperColumnAttribute>();
                if (columnAttribute?.ColumnName == null)
                    continue;

                if (columnAttribute.ColumnName.Trim() == "")
                    throw new DapperColumnEmptyException(type, property);

                columnToPropertyMapping[columnAttribute.ColumnName] = property;
            }

            CustomPropertyTypeMap map = new CustomPropertyTypeMap(type, (_, columnName) =>
            {
                PropertyInfo property = null;
                columnToPropertyMapping.TryGetValue(columnName, out property);
                return property;
            });

            SqlMapper.SetTypeMap(type, map);
        }
    }
}
