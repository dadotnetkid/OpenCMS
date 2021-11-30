using System;
using System.Text.Json.Serialization;

namespace OpenCMS.Shared.Models
{
    public class CatalogModel
    {
        

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("itemNumber")]
        public string ItemNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iBuyThisItem")]
        public bool IBuyThisItem { get; set; }

        [JsonPropertyName("iSellThisItem")]
        public bool ISellThisItem { get; set; }

        [JsonPropertyName("iInventoryThisItem")]
        public bool IInventoryThisItem { get; set; }

        [JsonPropertyName("salesAccount")]
        public string SalesAccount { get; set; }

        [JsonPropertyName("incomeAccount")]
        public string IncomeAccount { get; set; }

        [JsonPropertyName("inventoryAccount")]
        public string InventoryAccount { get; set; }
        [JsonPropertyName("lastCost")]
        public decimal? LastCost { get; set; }
        [JsonPropertyName("sellPrice")]
        public decimal? SellPrice { get; set; }
        [JsonPropertyName("quantity")]
        public decimal? Quantity { get; set; }
        [JsonPropertyName("perishable")]
        public bool Perishable { get; set; }
        public CatalogBuyingDetailsModel? PreviousBuyingDetails { get; set; } = new();
        public CatalogBuyingDetailsModel? BuyingDetails { get; set; } = new();
        public CatalogSellingDetailsModel SellingDetails { get; set; } = new();
        public string ManufacturerNo { get; set; }
        [JsonPropertyName("SKU")]
        public string SKU { get; set; }
    }

    public class CatalogBuyingDetailsModel
    {
        
        public int Id { get; set; }
        public int? CatalogId { get; set; }
        public decimal LastPurchasePrice { get; set; }
        public decimal StandardCost { get; set; }
        public string UOM { get; set; }
        public int BuyingUnit { get; set; }
        public decimal MinimumLevelRestockingAlert { get; set; }
        public decimal DefaultReorderQuantity { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class CatalogSellingDetailsModel
    {
        public int Id { get; set; }
        public int? CatalogId { get; set; }
        public decimal BaseSellingPrice { get; set; }
        public string UOM { get; set; }
        public int SellingUnit { get; set; }
        public decimal Retail { get; set; }
        public decimal WholeSale { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AppliedStartDate { get; set; }
    }
}
