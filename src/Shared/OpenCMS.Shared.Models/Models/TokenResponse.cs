using System;
using System.Text.Json.Serialization;

namespace OpenCMS.Shared.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expire")]
        public DateTime Expire { get; set; }
    }
}
