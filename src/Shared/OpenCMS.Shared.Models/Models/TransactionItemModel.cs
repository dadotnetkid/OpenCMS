using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Shared.Models
{
    public class TransactionItemModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("salesId")]
        public int? SalesId { get; set; }
        [JsonPropertyName("catalogId")]
        public int? CatalogId { get; set; }

        [JsonPropertyName("quantity")] public decimal? Quantity { get; set; } = 1;
        public int? DiscountId { get; set; }
        [JsonPropertyName("subTotal")]
        public decimal? SubTotal { get; set; }
        public string CreatedBy { get; set; }
        public bool? Deleted { get; set; }
        public bool IsNew { get; set; }
        public bool IsModified { get; set; }
        [JsonPropertyName("catalogs")]
        public virtual CatalogModel Catalogs { get; set; }
        [JsonPropertyName("expiryDate")]
        public DateTime? ExpiryDate { get; set; }
    }
}
