using System;

namespace DapperCodeFirstMappings.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperColumnAttribute : Attribute
    {
        public string ColumnName { get; }

        public DapperColumnAttribute(string columnName = null)
        {
            ColumnName = columnName;
        }
    }
}
