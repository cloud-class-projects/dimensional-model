using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace DimensionalModelETL.DataLayer.Model
{
    /// <summary>
    /// Entity for the Azure Table Storage contents
    /// </summary>
    public class ProductEntity : TableEntity
    {
        public int? ProductID { get; set; }
        public string Name { get; set; }
        public int? SubModel { get; set; }
        public string ProductNumber { get; set; }
        public int? MakeFlag { get; set; }
        public int? FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public int? SafetyStockLevel { get; set; }
        public int? ReorderPoint { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? ListPrice { get; set; }
        public int? Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public int? DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public int? ProductSubcategoryID { get; set; }
        public int? ProductModelID { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid? rowguid { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string SaleType { get; set; }
    }
    
}
