using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenCMS.Shared.Common;

namespace OpenCMS.Shared.Models
{
    public class TransactionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("cardFileId")]
        public int? CardFileId { get; set; }
        [JsonPropertyName("totalAmount")]
        public decimal? TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("transactionType")]
        public TransactionType TransactionType { get; set; }
        [JsonPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }
        public int? TenantId { get; set; }
        [JsonPropertyName("cardFile")]
        public virtual CardFilesModel CardFile{ get; set; }
        [JsonPropertyName("transactionItems")]
        public virtual ICollection<TransactionItemModel> TransactionItems { get; set; }

        public bool? HasItems { get; set; }
    }
}
