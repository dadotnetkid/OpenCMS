using System.Text.Json.Serialization;
using OpenCMS.Shared.Common;

namespace OpenCMS.Shared.Models
{
    public class CardFilesModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }
        public string CreatedBy { get; set; }
        public CardFileType CardFileType { get; set; }
    }
}
