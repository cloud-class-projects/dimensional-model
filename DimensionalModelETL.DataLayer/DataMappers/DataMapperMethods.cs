using System;
using System.Data;

namespace DimensionalModelETL.DataLayer.DataMappers
{
    /// <summary>
    /// Extension methods for the data mappers
    /// </summary>
    public static class DataMapperMethods
    {
        /// <summary>
        /// Converts the value from SQL Server to .NET
        /// </summary>
        public static T ReadValue<T>(this IDataRecord rec, string columnName)
        {
            var val = rec[columnName];
            Type t = val.GetType();
            if (typeof(T) == typeof(string))
            {
                if (val.ToString() == "NULL")
                {
                    return default(T);
                }
                else
                {
                    return (T)val;
                }
            }

            if (val == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)val;
            }
        }
    }
}
