using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Web.Infrastructure.Models
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

        [JsonPropertyName("salesAcount")]
        public string SalesAcount { get; set; }

        [JsonPropertyName("incomeAcount")]
        public string IncomeAcount { get; set; }

        [JsonPropertyName("inventoryAccount")]
        public string InventoryAccount { get; set; }
    }
}
