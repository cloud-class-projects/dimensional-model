using System;
using System.Data;
using System.Collections.Generic;

namespace DimensionalModelETL.DataLayer.DataMappers
{
    /// <summary>
    /// Class handles conversion to and from the ProductEntity class
    /// </summary>
    public class ProductMapper
    {
        /// <summary>
        /// Converts sql row to ProductEntity
        /// </summary>
        public static Model.ProductEntity ConvertToEntity(IDataRecord row, DateTime historyDate)
        {
            var ran = new Random().Next(0, 100);

            return new Model.ProductEntity
            {
                Class = row.ReadValue<string>("Class"),
                Color = row.ReadValue<string>("Color"),
                DaysToManufacture = row.ReadValue<int?>("DaysToManufacture"),
                DiscontinuedDate = row.ReadValue<DateTime?>("DiscontinuedDate"),
                FinishedGoodsFlag = row.ReadValue<int?>("FinishedGoodsFlag"),
                ListPrice = row.ReadValue<decimal?>("ListPrice"),
                MakeFlag = row.ReadValue<int?>("MakeFlag"),
                ModifiedDate = row.ReadValue<DateTime?>("ModifiedDate"),
                Name = row.ReadValue<string>("Name"),
                PartitionKey = row.ReadValue<int>("ProductId").ToString() + "_" + row.ReadValue<int>("SubModel").ToString(),
                ProductID = row.ReadValue<int?>("ProductID"),
                ProductLine = row.ReadValue<string>("ProductLine"),
                ProductModelID = row.ReadValue<int?>("ProductModelID"),
                ProductNumber = row.ReadValue<string>("ProductNumber"),
                ProductSubcategoryID = row.ReadValue<int?>("ProductSubcategoryID"),
                ReorderPoint = row.ReadValue<int?>("ReorderPoint"),
                RowKey = historyDate.ToString("yyyyMMdd"),
                SafetyStockLevel = row.ReadValue<int?>("SafetyStockLevel"),
                SellEndDate = row.ReadValue<DateTime?>("SellEndDate"),
                SellStartDate = row.ReadValue<DateTime?>("SellStartDate"),
                Size = row.ReadValue<int?>("Size"),
                SizeUnitMeasureCode = row.ReadValue<string>("SizeUnitMeasureCode"),
                StandardCost = row.ReadValue<decimal?>("StandardCost"),
                Style = row.ReadValue<string>("Style"),
                SubModel = row.ReadValue<int?>("SubModel"),
                Weight = row.ReadValue<decimal?>("Weight"),
                WeightUnitMeasureCode = row.ReadValue<string>("WeightUnitMeasureCode"),
                SaleType = ran < 10 ? "On Sale" : "Not on Sale"
            };
        }


        /// <summary>
        /// Converts sql row to comma seperated values
        /// </summary>
        public static string ConvertToCSV(IDataRecord row, DateTime historyDate)
        {
            var ran = new Random().Next(0, 100);

            return string.Join(","
                , row.ReadValue<string>("Class"),
                row.ReadValue<string>("Color"),
                row.ReadValue<int?>("DaysToManufacture"),
                row.ReadValue<DateTime?>("DiscontinuedDate"),
                row.ReadValue<int?>("FinishedGoodsFlag"),
                row.ReadValue<decimal?>("ListPrice"),
                row.ReadValue<int?>("MakeFlag"),
                row.ReadValue<DateTime?>("ModifiedDate"),
                row.ReadValue<string>("Name"),
                row.ReadValue<int>("ProductId").ToString() + "_" + row.ReadValue<int>("SubModel").ToString(),
                row.ReadValue<int?>("ProductID"),
                row.ReadValue<string>("ProductLine"),
                row.ReadValue<int?>("ProductModelID"),
                row.ReadValue<string>("ProductNumber"),
                row.ReadValue<int?>("ProductSubcategoryID"),
                row.ReadValue<int?>("ReorderPoint"),
                historyDate.ToString("yyyyMMdd"),
                row.ReadValue<int?>("SafetyStockLevel"),
                row.ReadValue<DateTime?>("SellEndDate"),
                row.ReadValue<DateTime?>("SellStartDate"),
                row.ReadValue<int?>("Size"),
                row.ReadValue<string>("SizeUnitMeasureCode"),
                row.ReadValue<decimal?>("StandardCost"),
                row.ReadValue<string>("Style"),
                row.ReadValue<int?>("SubModel"),
                row.ReadValue<decimal?>("Weight"),
                row.ReadValue<string>("WeightUnitMeasureCode"),
                ran < 10 ? "On Sale" : "Not on Sale")
                + Environment.NewLine;
        }


        /// <summary>
        /// Converts entity to comma seperated values
        /// </summary>
        public static string ConvertToCSV(Model.ProductEntity entity)
        {
            var ran = new Random().Next(0, 100);

            return string.Join(",",
                entity.ProductID,
                entity.Name,
                entity.SubModel,
                entity.ProductNumber,
                entity.MakeFlag,
                entity.FinishedGoodsFlag,
                entity.Color,
                entity.SafetyStockLevel,
                entity.ReorderPoint,
                entity.StandardCost,
                entity.ListPrice,
                entity.Size,
                entity.SizeUnitMeasureCode,
                entity.WeightUnitMeasureCode,
                entity.Weight,
                entity.DaysToManufacture,
                entity.ProductLine,
                entity.Class,
                entity.Style,
                entity.ProductSubcategoryID,
                entity.ProductModelID,
                entity.SellStartDate,
                entity.SellEndDate,
                entity.DiscontinuedDate,
                entity.rowguid,
                entity.ModifiedDate,
                entity.SaleType
                );
        }


        /// <summary>
        /// Converts entity into a format that can be read by the Gateway_SQLServer class
        /// </summary>
        public static Dictionary<string, object> ConvertToSQLRow(Model.ProductEntity entity, DateTime start, DateTime end, int isCurrent)
        {
            return new Dictionary<string, object>
            {
                {"ProductID",entity.ProductID},
                {"Name",entity.Name},
                {"SubModel",entity.SubModel},
                {"ProductNumber",entity.ProductNumber},
                {"MakeFlag",entity.MakeFlag},
                {"FinishedGoodsFlag",entity.FinishedGoodsFlag},
                {"Color",entity.Color},
                {"SafetyStockLevel",entity.SafetyStockLevel},
                {"ReorderPoint",entity.ReorderPoint},
                {"StandardCost",entity.StandardCost},
                {"ListPrice",entity.ListPrice},
                {"Size",entity.Size},
                {"SizeUnitMeasureCode",entity.SizeUnitMeasureCode},
                {"WeightUnitMeasureCode",entity.WeightUnitMeasureCode},
                {"Weight",entity.Weight},
                {"DaysToManufacture",entity.DaysToManufacture},
                {"ProductLine",entity.ProductLine},
                {"Class",entity.Class},
                {"Style",entity.Style},
                {"ProductSubcategoryID",entity.ProductSubcategoryID},
                {"ProductModelID",entity.ProductModelID},
                {"SellStartDate",entity.SellStartDate},
                {"SellEndDate",entity.SellEndDate},
                {"DiscontinuedDate",entity.DiscontinuedDate},
                {"rowguid",entity.rowguid},
                {"ModifiedDate",entity.ModifiedDate},
                {"SaleType",entity.SaleType},
                {"SKeyEffectiveStartDate", start},
                {"SKeyEffectiveEndDate", end},
                {"SKeyIsCurrent", isCurrent}
            };
        }
    }
}
