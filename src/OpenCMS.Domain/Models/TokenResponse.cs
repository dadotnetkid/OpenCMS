using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Domain.Models
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
