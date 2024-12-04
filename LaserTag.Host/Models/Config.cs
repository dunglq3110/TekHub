using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace TekHub.Host.Models
{
    public class Config
    {
        [JsonPropertyName("config_id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code_name")]
        public string CodeName { get; set; } = string.Empty;

        [JsonPropertyName("config_type")]
        public SharedBase ConfigType { get; set; }

        [JsonPropertyName("value1")]
        public string Value1 { get; set; } = string.Empty;

        [JsonPropertyName("value2")]
        public string Value2 { get; set; } = string.Empty;

        [JsonPropertyName("value3")]
        public string Value3 { get; set; } = string.Empty;

        [JsonPropertyName("value4")]
        public string Value4 { get; set; } = string.Empty;

        [JsonPropertyName("value5")]
        public string Value5 { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} ({CodeName}): {Value1}";
        }
    }

}
