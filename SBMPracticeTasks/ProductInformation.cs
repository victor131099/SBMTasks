﻿namespace SBMPracticeTasks
{
    public class ProductInformation
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public short ReorderPoint { get; set; }
        public string? Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }

        public int DaysToManufacture { get; set; }
        public DateTime SellStartDate { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
