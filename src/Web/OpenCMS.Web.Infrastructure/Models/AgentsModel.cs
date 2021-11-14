using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Web.Infrastructure.Models
{
    public class AgentsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }
}
