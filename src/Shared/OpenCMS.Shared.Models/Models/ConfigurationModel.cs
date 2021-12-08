using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenCMS.Shared.Models
{
    public class ConfigurationModel
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
    }
    public class HashConfigurationModel
    {
        [JsonPropertyName("hashConfiguration")]
        public string HashConfiguration { get; set; }
    }
}
