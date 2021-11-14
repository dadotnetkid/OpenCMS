using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Web.Infrastructure.Models
{
    public class AccountsModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("parentId")]
        public string ParentId { get; set; }

        [JsonPropertyName("classificationId")]
        public string ClassificationId { get; set; }

        [JsonPropertyName("isHeader")]
        public bool IsHeader { get; set; }

        [JsonPropertyName("isDetail")]
        public bool IsDetail { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("accountName")]
        public string AccountName { get; set; }

        [JsonPropertyName("accountNumber")]
        public decimal? AccountNumber { get; set; }

        [JsonPropertyName("openingBalance")]
        public decimal? OpeningBalance { get; set; }

        [JsonPropertyName("currentBalance")]
        public object CurrentBalance { get; set; }

        [JsonPropertyName("hierarchyLevel")]
        public int HierarchyLevel { get; set; }

        [JsonPropertyName("orderBy")]
        public int OrderBy { get; set; }

        [JsonPropertyName("createdOnUtc")]
        public DateTime CreatedOnUtc { get; set; }

        [JsonPropertyName("updateOnUtc")]
        public DateTime UpdateOnUtc { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("updatedBy")]
        public string UpdatedBy { get; set; }
    }
}
