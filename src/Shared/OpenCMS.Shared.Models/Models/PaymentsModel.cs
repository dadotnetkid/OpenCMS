using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Shared.Models
{
    public class PaymentsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("transactionId")]
        public int TransactionId { get; set; }
        [JsonPropertyName("paymentMethod")] 
        public int? PaymentMethod { get; set; } = 1;

        public int? Term { get; set; } = 1;
        public decimal? PaymentAmount { get; set; } = 0;
        public TransactionModel Transaction { get; set; } = new();
        public string CheckNumber { get; set; }
        public decimal? CheckAmount { get; set; }
        public string Bank { get; set; }
        public string TransactionNumber { get; set; }
    }
}
