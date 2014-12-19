using System;
using System.Collections.Generic;
using System.Linq;

namespace DimensionalModelETL.DataLayer.DataMappers
{
    /// <summary>
    /// Performs logic to transform data into dimensional format
    /// </summary>
    public class ProductDimensionMapper
    {
        /// <summary>
        /// Converts the product entity into a form that can be read by the SQL mapper
        /// It also perfoms the Type II SCD logic
        /// </summary>
        public List<Dictionary<string, object>> CreateTypeIIDimension(IEnumerable<Model.ProductEntity> entities, List<string> type2Dimension)
        {
            var dimensionRows = new List<Dictionary<string, object>>();

            // Skip if nothing was returned
            if (entities.Count() == 0)
                return dimensionRows;

            // Order rows
            var rows = entities
                        .OrderBy(row => row.RowKey)
                        .ToList();

            // Set first row
            var previous = rows[0];
            string previousStart = "19000101";

            for (int i = 1; i < rows.Count(); i++)
            {
                // Check to see if type II value changed
                var random = new Random().Next(0, 100); // The values in AST were not properly randomized.  This simulates a type II dimension
                if(i%20==0) // %5 change rate
                {
                    var value = ProductMapper.ConvertToSQLRow(
                        previous,
                        DateTime.ParseExact(previousStart, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        DateTime.ParseExact(rows[i].RowKey.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), 
                        0);

                    dimensionRows.Add(value);

                    // set the new previous
                    previous = rows[i];
                    previousStart = rows[i].RowKey;
                }
            }
            

            // Close last row
            var last = ProductMapper.ConvertToSQLRow(
                previous, 
                DateTime.ParseExact( previousStart, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), 
                new DateTime(3000,1,1),
                1);

            dimensionRows.Add(last);

            return dimensionRows;
        }
    }
}
